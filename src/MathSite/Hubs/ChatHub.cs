using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Common.Extensions;
using MathSite.Facades.Conversations;
using MathSite.Facades.Messages;
using MathSite.Facades.Users;
using MathSite.Hubs.ConnectionsMapper;
using MathSite.ViewModels.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MathSite.Hubs
{

    [Authorize]
    public class ChatHub : Hub
    {
        private static readonly ConnectionsMapper<Guid> Connections = new ConnectionsMapper<Guid>();
        private IUsersFacade UsersFacade { get; }
        private IMessagesFacade MessagesFacade { get; }
        private IConversationsFacade ConversationFacade { get; }
        public ChatHub(IUsersFacade usersFacade,
            IMessagesFacade messagesFacade, IConversationsFacade conversationFacade)
        {
            UsersFacade = usersFacade;
            MessagesFacade = messagesFacade;
            ConversationFacade = conversationFacade;
        }

        public async Task CreateGroupConversation(CreateGroupConversationViewModel conversation)
        {
            if (string.IsNullOrWhiteSpace(conversation.Name) || conversation.Name.Length >= 20) return;

            var currentUserId = GetCurrentUserId();

            if (currentUserId == Guid.Empty) return;

            var members = conversation.MembersIds.Distinct().Where(memberId => memberId != currentUserId).ToList();

            for (var i = members.Count - 1; i > -1; i--)
            {
                if (await UsersFacade.DoesUserExistsAsync(members[i])) continue;
                members.RemoveAt(i);
            }
            if (members.Count == 0) return;

            if (members.Count == 1)
            {
                var member = await UsersFacade.GetUserAsync(members[0]);
                await CreatePrivateConversation(member.Login);
                return;
            }

            var currentUserConnections = Connections.GetConnections(currentUserId);

            var conversationId =
                await ConversationFacade.CreateConversationAndGetIdAsync(currentUserId, conversation.Name, "Group");

            var conversationViewModel = new ConversationViewModel
            {
                Id = conversationId.ToString(),
                Name = conversation.Name,
                CreatorsId = currentUserId.ToString(),
                Type = "Group"
            };

            foreach (var connection in currentUserConnections)
            {
                await Clients.Client(connection).SendAsync("GetNewConversation", conversationViewModel);
            }

            foreach (var memberId in members)
            {
                if (memberId == currentUserId) continue;
                await ConversationFacade.AddUserToConversationAsync(memberId, conversationId);
                var memberConnections = Connections.GetConnections(memberId);
                foreach (var connection in memberConnections)
                {
                    await Clients.Client(connection)
                        .SendAsync("GetNewConversation", conversationViewModel);
                }
            }
        }

        public async Task CreatePrivateConversation(string interlocutorLogin)
        {

            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            var interlocutor = await UsersFacade.GetUserByLoginAsync(interlocutorLogin);

            if (interlocutor == null || currentUserId == interlocutor.Id) return;

            if (await ConversationFacade.DoesPrivateConversationExistsAsync(currentUserId, interlocutor.Id)) return;

            var currentUserConnections = Connections.GetConnections(currentUserId);

            var conversationId =
                await ConversationFacade.CreateConversationAndGetIdAsync(currentUserId, String.Empty, "Private");

            var conversationViewModel = new ConversationViewModel
            {
                Id = conversationId.ToString(),
                Name = interlocutor.Person.Surname + " " + interlocutor.Person.Name,
                InterlocutorLogin = interlocutor.Login,
                CreatorsId = currentUserId.ToString(),
                Type = "Private"
            };

            foreach (var connection in currentUserConnections)
            {
                await Clients.Client(connection).SendAsync("GetNewConversation", conversationViewModel);
            }

            var currentUser = await UsersFacade.GetUserAsync(currentUserId);

            conversationViewModel.Name = currentUser.Person.Surname + " " + currentUser.Person.Name;
            conversationViewModel.InterlocutorLogin = currentUser.Login;

            await ConversationFacade.AddUserToConversationAsync(interlocutor.Id, conversationId);
            var memberConnections = Connections.GetConnections(interlocutor.Id);
            foreach (var connection in memberConnections)
            {
                await Clients.Client(connection)
                    .SendAsync("GetNewConversation", conversationViewModel);
            }
        }

        public async Task AddPossibleMember(string login)
        {
            var user = await UsersFacade.GetUserByLoginAsync(login);
            if (user.IsNull()) return;
            var currentUserId = GetCurrentUserId();

            if (currentUserId == Guid.Empty) return;
            if (user.Id == currentUserId) return;

            var memberViewModel = new ConversationMemberViewModel
            {
                Id = user.Id.ToString(),
                Name = user.Person.Surname + " " + user.Person.Name,
                Login = user.Login
            };
            await Clients.Caller.SendAsync("AddPossibleMember", memberViewModel);
        }

        public async Task AddNewMember(string login, Guid conversationId)
        {

            var user = await UsersFacade.GetUserByLoginAsync(login);
            if (user.IsNull())
                return;

            var conversation = await ConversationFacade.GetConversationAsync(conversationId);
            var currentUserId = GetCurrentUserId();

            if (currentUserId == Guid.Empty) return;

            if (conversation.IsNull() || conversation.Type == "Private" ||
                !await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId) ||
                await ConversationFacade.DoesConversationContainsMemberAsync(user.Id, conversation.Id))
            {
                return;
            }

            await ConversationFacade.AddUserToConversationAsync(user.Id, conversationId);
            var conversationViewModel = new ConversationViewModel
            {
                Id = conversationId.ToString(),
                Name = conversation.Name,
                CreatorsId = conversation.CreatorId.ToString()
            };

            var connections = Connections.GetConnections(user.Id);

            foreach (var connection in connections)
                await Clients.Client(connection).SendAsync("GetNewConversation", conversationViewModel);

            var status = Connections.IsUserOnline(user.Id)
                ? "Online"
                : "Offline";

            var member = new ConversationMemberViewModel
            {
                Id = user.Id.ToString(),
                Name = user.Person.Surname + " " + user.Person.Name,
                Login = user.Login,
                Status = status
            };

            await Clients.Group(conversationId.ToString()).SendAsync("AddNewMember", member);
        }

        public async Task ChangeConversation(Guid? currentConversationId, Guid newConversationId)
        {
            var currentConversationIdGuid = currentConversationId ?? Guid.Empty;
            var currentUserId = GetCurrentUserId();

            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.DoesConversationExistsAsync(newConversationId) ||
                !await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, newConversationId)) return;

            var user = await UsersFacade.GetUserAsync(currentUserId);

            if (user == null) return;
            if (currentConversationIdGuid != Guid.Empty)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentConversationId.ToString());
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, newConversationId.ToString());

            await MessagesFacade.SetAllMessagesRead(currentUserId, newConversationId);

            var connections = Connections.GetConnections(currentUserId);
            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("SetRead", newConversationId);
            }

        }

        public async Task KickMember(Guid conversationId, Guid userId)
        {

            if (await ConversationFacade.GetConversationsTypeAsync(conversationId) == "Private") return;

            if (!await ConversationFacade.DoesConversationContainsMemberAsync(userId, conversationId)) return;

            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.IsConversationsCreatorAsync(currentUserId, conversationId)) return;
            if (await ConversationFacade.IsConversationsCreatorAsync(userId, conversationId)) return;

            await ConversationFacade.RemoveMemberAsync(conversationId, userId);
            var connections = Connections.GetConnections(userId);
            foreach (var connection in connections)
            {
                await Groups.RemoveFromGroupAsync(connection, conversationId.ToString());
                await Clients.Client(connection).SendAsync("RemoveConversation", conversationId);
            }
            await Clients.Group(conversationId.ToString()).SendAsync("RemoveMember", userId);


        }

        public async Task GetMembers(Guid conversationId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId)) return;

            var users = await UsersFacade.GetUsersByConversationIdAsync(conversationId);
            var creator = await UsersFacade.GetConversationCreatorAsync(conversationId);

            var members = new List<ConversationMemberViewModel>();
            ConversationMemberViewModel creatorViewModel = null;
            foreach (var user in users)
            {
                var status = Connections.IsUserOnline(user.Id)
                    ? "Online"
                    : "Offline";
                var conversationMemberViewModel = new ConversationMemberViewModel
                {
                    Id = user.Id.ToString(),
                    Name = user.Person.Surname + " " + user.Person.Name,
                    Login = user.Login,
                    Status = status

                };
                if (user.Id == creator.Id)
                {
                    creatorViewModel = conversationMemberViewModel;
                }
                else
                {
                    members.Add(conversationMemberViewModel);
                }
            }

            var conversationMembersViewModel =
                new ConversationMembersViewModel { Members = members, Creator = creatorViewModel };

            await Clients.Caller.SendAsync("GetMembers", conversationMembersViewModel);

        }

        public async Task SendMessage(Guid conversationId, string messageBody)
        {
            if (messageBody.IsNullOrWhiteSpace() && messageBody.Length >= 1000) return;
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId)) return;

            var messageInfo =
                await MessagesFacade.CreateMessageAndGetIdWithCreationDateAsync(currentUserId, conversationId,
                    messageBody);

            await MessagesFacade.SetMessageRead(currentUserId, messageInfo.Item1);

            var user = await UsersFacade.GetUserAsync(currentUserId);
            var messageViewModel = new MessageViewModel
            {
                Id = messageInfo.Item1.ToString(),
                Author = $"{user.Person.Name} {user.Person.Surname}",
                Body = messageBody,
                CreationDate = messageInfo.Item2.ToString(CultureInfo.InvariantCulture)
            };

            await Clients.Group(conversationId.ToString()).SendAsync("GetMessage", messageViewModel);

            var currentUserConnections = Connections.GetConnections(currentUserId);

            foreach (var connection in currentUserConnections)
            {
                if (connection == Context.ConnectionId) continue;
                await Clients.Client(connection).SendAsync("ConversationUp", conversationId);
            }

            var members = await UsersFacade.GetUsersByConversationIdAsync(conversationId);

            foreach (var member in members)
            {
                if (member.Id == currentUserId) continue;
                var connections = Connections.GetConnections(member.Id);
                foreach (var connection in connections)
                {
                    await Clients.Client(connection).SendAsync("NewUnreadMessage", conversationId);
                }
            }
        }

        public async Task GetConversationHistory(Guid conversationId, Guid? firstMessageId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId)) return;

            var firstMessageIdGuid = firstMessageId ?? Guid.Empty;
            var history = await MessagesFacade.GetMessagesByConversationIdAsync(conversationId, firstMessageIdGuid, 10);
            var messagesViewModel = history.Select(msg => new MessageViewModel
            {
                Id = msg.Id.ToString(),
                Author = msg.Author.Person.Name + " " + msg.Author.Person.Surname,
                Body = msg.Body,
                CreationDate = msg.CreationDate.ToString(CultureInfo.InvariantCulture)
            });
            await Clients.Caller.SendAsync("GetHistory", messagesViewModel);

        }

        public async Task LeaveConversation(Guid conversationId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId))
            {
                var connections = Connections.GetConnections(currentUserId);
                foreach (var connection in connections)
                {
                    await Groups.RemoveFromGroupAsync(connection, conversationId.ToString());
                    await Clients.Client(connection).SendAsync("RemoveConversation", conversationId);
                }

                var membersCount = await UsersFacade.GetConversationsMembersCountAsync(conversationId);
                if (membersCount == 1)
                {
                    await ConversationFacade.RemoveConversationAsync(conversationId);
                    return;
                }
                await ConversationFacade.RemoveMemberAsync(conversationId, currentUserId);
                await Clients.Group(conversationId.ToString()).SendAsync("RemoveMember", currentUserId);
            }
        }

        public async Task SetMessageRead(Guid messageId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            var conversation = await ConversationFacade.GetConversationByMessageIdAsync(messageId);
            if (!await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversation.Id)) return;
            await MessagesFacade.SetMessageRead(currentUserId, messageId);
            var connections = Connections.GetConnections(currentUserId);

            foreach (var connection in connections)
            {
                if (connection == Context.ConnectionId) continue;
                await Clients.Client(connection).SendAsync("SetRead", conversation.Id);
            }
        }

        public async Task SetAllMessagesRead(Guid conversationId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            if (!await ConversationFacade.DoesConversationContainsMemberAsync(currentUserId, conversationId)) return;
            await MessagesFacade.SetAllMessagesRead(currentUserId, conversationId);
            var connections = Connections.GetConnections(currentUserId);
            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("SetRead", conversationId);
            }
        }

        private Guid GetCurrentUserId()
        {
            if (!Context.User.Identity.IsAuthenticated)
                return Guid.Empty;

            return Guid.Parse(Context.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypeConstants.UserId)?.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            var user = await UsersFacade.GetUserAsync(currentUserId);
            var userViewModel = new ChatUserViewModel
            {
                Id = user.Id.ToString(),
                Name = user.Person.Surname + ' ' + user.Person.Name,
                Login = user.Login
            };
            await Clients.Caller.SendAsync("GetUserInfo", userViewModel);


            var conversations = await ConversationFacade.GetConversationsByUserIdAsync(currentUserId);
            if (!Connections.IsUserOnline(currentUserId))
            {
                foreach (var conversation in conversations)
                {
                    await Clients.Group(conversation.Id.ToString()).SendAsync("SetUserOnline", currentUserId);
                }
            }

            Connections.Add(currentUserId, Context.ConnectionId);

            var conversationsViewModel = new List<ConversationViewModel>();
            foreach (var conversation in conversations)
            {
                var viewModel = new ConversationViewModel
                {
                    Id = conversation.Id.ToString(),
                    CreatorsId = conversation.CreatorId.ToString(),
                    UnreadMessagesCount = await MessagesFacade.UnreadMessagesCount(currentUserId, conversation.Id),
                    Type = conversation.Type
                };
                if (conversation.Type == "Group")
                {
                    viewModel.Name = conversation.Name;
                }
                else
                {
                    var members = await UsersFacade.GetUsersByConversationIdAsync(conversation.Id);
                    var interlocutor = members.First(member => member.Id != currentUserId);
                    viewModel.Name = interlocutor.Person.Surname + " " + interlocutor.Person.Name;
                    viewModel.InterlocutorLogin = interlocutor.Login;
                }
                conversationsViewModel.Add(viewModel);
            }
            await Clients.Caller.SendAsync("GetConversations", conversationsViewModel);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty) return;

            Connections.Remove(currentUserId, Context.ConnectionId);
            if (Connections.IsUserOnline(currentUserId))
            {
                await base.OnDisconnectedAsync(ex);
                return;
            }

            var conversations = await ConversationFacade.GetConversationsByUserIdAsync(currentUserId);
            foreach (var conversation in conversations)
            {
                await Clients.Group(conversation.Id.ToString()).SendAsync("SetUserOffline", currentUserId);
            }

            await base.OnDisconnectedAsync(ex);

        }

    }

}



