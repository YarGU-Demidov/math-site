var inProgress = false;
var notShownMessagesExist = true;

jQuery.validator.addMethod("isNewMember",
    function (element) {
        if ($(".members").children('[data-login="' + element + '"]').length > 0) {
            return false;
        }
        else {
            return true;
        }

    }, "Пользователь уже состоит в беседе");

jQuery.validator.addMethod("isNotCurrentMember",
    function (element) {
        var currentUserLogin = $('.current-user').data("login");
        if (element === currentUserLogin) {
            return false;
        }
        else {
            return true;
        }

    }, "Нельзя добавить себя в беседу");

jQuery.validator.addMethod("isNewPossibleMember",
    function (element) {
        if ($(".possible-members").children('[data-login="' + element + '"]').length > 0) {
            return false;
        }
        else {
            return true;
        }

    }, "Пользователь уже добавлен");

jQuery.validator.addMethod("isNewPrivateConversation",
    function (element) {
        if ($(".conversations").children('[data-type= "Private"][data-login = "' + element + '"]').length > 0) {
            return false;
        }
        else {
            return true;
        }

    }, "Такая личная беседа уже существует");


$(function () {

    $("#add-member-modal form").validate({
        errorClass: "text-danger",
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        },
        rules:
            {
                login:
                    {
                        required: true,
                        remote: {
                            url: "/account/checklogin",
                            dataType: "json",
                            data:
                                {
                                    login: function () {
                                        return $('#new-member-login').val();
                                    }
                                }
                        },
                        isNewMember: true


                    }
            },
        messages:
            {
                login:
                    {
                        required: "Введите логин"
                    }
            }
    });

    $("#create-group-modal form").validate({
        errorClass: "text-danger",
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            error.insertAfter($("label[for='" + element.attr('id') + "']"));
        },
        rules:
            {
                conversationName:
                    {
                        required: true,
                        maxlength: 20
                    },
                possibleMemberLogin:
                    {
                        required: true,
                        remote: {
                            url: "/account/checklogin",
                            dataType: "json",
                            data:
                                {
                                    login: function () {
                                        return $('#possible-member-login').val();
                                    }
                                }
                        },
                        isNewPossibleMember: true,
                        isNotCurrentMember: true
                    }

            },
        messages:
            {
                conversationName:
                    {
                        required: "Введите имя",
                        maxlength: "Максимально допустимое количество символов: 20"
                    },
                possibleMemberLogin:
                    {
                        required: "Введите имя"
                    }
            }
    });

    $("#create-private-conversation-modal form").validate({
        errorClass: "text-danger",
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        },
        rules:
            {
                login:
                    {
                        required: true,
                        remote: {
                            url: "/account/checklogin",
                            dataType: "json",
                            data:
                                {
                                    login: function () {
                                        return $('#interlocutor-login').val();
                                    }
                                }
                        },
                        isNotCurrentMember: true,
                        isNewPrivateConversation: true

                    }

            },
        messages:
            {
                login:
                    {
                        required: "Введите логин",
                        isNotCurrentMember: "Нельзя начать беседу с самим собой"
                    }
            }
    });


    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on('GetConversations',
        (conversations) => {
            for (var i = 0; i < conversations.length; i++) {
                var conversation = $('<div/>',
                    {
                        'class': "conversation border border-secondary rounded",
                        id: conversations[i].id,
                        'data-creators-id': conversations[i].creatorsId,
                        text: conversations[i].name,
                        'data-type': conversations[i].type
                    });
                if (conversations[i].type === "Private") {
                    $(conversation).attr('data-login', conversations[i].interlocutorLogin);
                }
                if (conversations[i].unreadMessagesCount > 0) {
                    var unreadMessagesCount = $('<span/>',
                        {
                            'class': "badge badge-secondary",
                            text: conversations[i].unreadMessagesCount
                        });
                    conversation.append(unreadMessagesCount);
                }
                $(".conversations").append(conversation);
            }
        });

    connection.on('GetNewConversation',
        (conversation) => {

            var newConversation = $('<div/>',
                {
                    'class': "conversation border border-secondary rounded",
                    id: conversation.id,
                    'data-creators-id': conversation.creatorsId,
                    text: conversation.name,
                    'data-type': conversation.type,
                });
            if (conversation.type === "Private") {
                $(newConversation).attr('data-login', conversation.interlocutorLogin);
            }

            if (conversation.unreadMessagesCount > 0) {
                var unreadMessagesCount = $('<span/>',
                    {
                        'class': "badge badge-secondary",
                        text: conversation.unreadMessagesCount
                    });
                newConversation.append(unreadMessagesCount);
            }

            $(".conversations").prepend(newConversation);
        });

    $("#create-group").click(function () {
        var isFormValid = $("#group-name").valid();
        var isPossibleUsersValid = validatePossibleUsers();
        if (isFormValid && isPossibleUsersValid) {
            $('#create-group-modal .text-danger').text('');
            var members = $("#create-group-modal .possible-member").map(function () {
                return this.id;
            }).get();
            var newConversation = {
                Name: $("#group-name").val(),
                MembersIds: members
            };
            connection.invoke("CreateGroupConversation", newConversation);
            $('#create-group-modal').modal('toggle');
        }
    });

    $("#create-private").click(function () {
        var isFormValid = $("form[id = 'interlocutor-form']").valid();
        if (isFormValid) {
            var interlocutorLogin = $("#interlocutor-login").val();
            connection.invoke("CreatePrivateConversation", interlocutorLogin);
            $('#create-private-conversation-modal').modal('toggle');
        }
    });

    $("#leave-conversation").click(function () {
        var currentConversationId = $(".current-conversation").attr('id');
        connection.invoke("LeaveConversation", currentConversationId);
    });

    connection.on('RemoveConversation', (conversationId) => {
        $(".conversations [id = '" + conversationId + "']").remove();
        var currentConversationId = $(".current-conversation").attr('id');
        if (currentConversationId === conversationId) {
            $(".chat-body").off('scroll', scrollHandler);
            $(".members").empty();
            $(".current-conversation").attr("id", '');
            $(".current-conversation").attr("data-creators-id", '');
            $('.current-conversation').text('');
            $(".chat-body").empty();
            $("#leave-conversation").prop('disabled', true);
            $(".add-member").prop('disabled', true);
            $("#send-message").prop('disabled', true);
            $("#message").prop('disabled', true);
        }
    });

    connection.on('GetUserInfo',
        (userViewModel) => {
            var user = $('.current-user');
            user.attr('id', userViewModel.id);
            user.data("login", userViewModel.login);
            user.append("<h5>" + userViewModel.name + "</h5>");
            user.data("login", userViewModel.login);
        }
    );
    connection.on('GetMembers',
        (membersViewModel) => {

            var newMember;
            if (membersViewModel.creator !== null) {
                $(".members").append("<h5> Создатель </h5>");

                newMember = $('<div/>',
                    {
                        id: membersViewModel.creator.id,
                        'data-login': membersViewModel.creator.login,
                        'class': 'creator border border-secondary rounded'
                    });

                $(newMember).append($('<div/>',
                    {
                        'class': 'name',
                        text: membersViewModel.creator.name
                    }));
                $(newMember).append($('<span/>',
                    {
                        'class': 'status ' + membersViewModel.creator.status.toLowerCase(),
                        text: membersViewModel.creator.status
                    }));
                $(".members").append(newMember);
            }
            var currentConversationId = $('.current-conversation').attr('id');
            var currentConversationType = $('.conversations').children("[id = '" + currentConversationId + "']").data('type');

            var removeSpan = '';
            if (currentConversationType !== 'Private') {
                var currentUserId = $(".current-user").attr('id');
                if (membersViewModel.creator !== null && membersViewModel.creator.id === currentUserId) {
                    removeSpan = $('<span/>',
                        {
                            'class': 'remove border border-secondary rounded',
                            text: 'X'
                        });
                }
            }

            for (var i = 0; i < membersViewModel.members.length; i++) {

                newMember = $('<div/>',
                    {
                        id: membersViewModel.members[i].id,
                        'data-login': membersViewModel.members[i].login,
                        'class': 'member border border-secondary rounded'
                    });

                $(newMember).append($('<div/>',
                    {
                        'class': 'name',
                        text: membersViewModel.members[i].name
                    }));
                $(newMember).append($('<span/>',
                    {
                        'class': 'status ' + membersViewModel.members[i].status.toLowerCase(),
                        text: membersViewModel.members[i].status
                    }));
                $(newMember).prepend($(removeSpan).clone(false));
                $(".members").append(newMember);
            }
        }
    );
    connection.on('AddNewMember',
        (member) => {

            var currentConversationCreatorId = $('.current-conversation').attr("data-creators-id");

            var newMember = $('<div/>',
                {
                    id: member.id,
                    'data-login': member.login,
                    'class': 'border border-secondary rounded'
                });

            $(newMember).append($('<div/>',
                {
                    'class': 'name',
                    text: member.name
                }));
            $(newMember).append($('<span/>',
                {
                    'class': 'status ' + member.status,
                    text: member.status
                }));

            if (currentConversationCreatorId === member.id) {
                $(newMember).addClass("creator");
                $(".members").prepend(newMember);
                $(".members").prepend('<h5> Создатель </h5>');

            } else {
                $(newMember).addClass("member");
                var currentUserId = $(".current-user").attr('id');
                if (currentConversationCreatorId === currentUserId) {
                    var removeSpan = $('<span/>',
                        {
                            'class': 'remove border border-secondary rounded',
                            text: 'X'
                        });
                    $(newMember).prepend(removeSpan);
                }
                $(".members").append(newMember);
            }
        });

    $("#add-new-member").click(function () {
        var isFormValid = $("form[id = 'new-member-form']").valid();
        if (isFormValid) {
            var currentConversation = $('.current-conversation').attr('id');
            var newMemberLogin = $("#new-member-login").val();
            connection.invoke("AddNewMember", newMemberLogin, currentConversation);
            $('#add-member-modal').modal('toggle');
        }


    });

    connection.on('RemoveMember',
        (memberId) => {
            var currentConversationCreatorId = $('.current-conversation').attr("data-creators-id");
            if (memberId === currentConversationCreatorId) {
                $(".members h5").remove();
            }
            $(".members [id='" + memberId + "']").remove();
        });

    $('.members').on('click',
        ".member .remove",
        function () {
            var userId = $(this).parent().attr('id');
            var currentConversationId = $(".current-conversation").attr("id");
            connection.invoke('KickMember', currentConversationId, userId);
        });

    connection.on("AddPossibleMember",
        (member) => {          
            var possibleMember = $('<div/>',
                {
                    'class': "possible-member border border-secondary rounded",
                    id: member.id,
                    'data-login': member.login,
                    text: member.login + ' : ' + member.name
                });
            $(possibleMember).append($('<span/>',
                {
                    'class': "remove border border-secondary rounded",
                    text: 'X'
                }));

            $("#create-group-modal .possible-members").append(possibleMember);

            if ($("#create-group-modal .possible-members").children().length === 2) {
                $('#possible-users-error').remove();
            }
            $("#possible-member-login-error").text("");
            
        });

    $("#add-possible-member").click(function () {
        var isFormValid = $("#possible-member-login").valid();
        if (isFormValid) {
            var memberlogin = $("#possible-member-login");
            connection.invoke("AddPossibleMember", memberlogin.val());
            memberlogin.val("");
        }
    });

    $('#create-group-modal').on('click', ".possible-member .remove", function () {
        var user = $(this).parent();
        user.remove();
        var possibleMembers = $(".possible-members");

        validatePossibleUsers();
    });

    connection.on('GetHistory',
        (history) => {
            if (history.length === 0) {
                notShownMessagesExist = false;
            } else {
                var currentTopElement = $('.message:first-child');
                for (var i = 0; i < history.length; i++) {

                    var message = $('<div/>', { 'class': "message border border-secondary rounded", id: history[i].id })
                        .append(
                            $('<h5/>', { text: history[i].author })).append(
                                $('<p/>', { text: history[i].body })).append(
                                    $('<span/>', { 'class': 'time', text: history[i].creationDate }));

                    $(".chat-body").prepend(message);
                }
                if (currentTopElement.length === 0) {
                    $(".chat-body").scrollTop($(".chat-body")[0].scrollHeight);
                } else {
                    var previousHeight = 0;
                    currentTopElement.prevAll(function () {
                        previousHeight += $(this).outerHeight();
                    });
                    currentTopElement.prevAll().each(function () {
                        previousHeight += $(this).outerHeight();
                    });
                    $(".chat-body").scrollTop(previousHeight);
                }
                $(".chat-body").on('scroll', scrollHandler);
            }
            inProgress = false;
        });

    connection.on("GetMessage",
        (message) => {
            var chatBody = $(".chat-body");
            var isBottom = false;
            if (chatBody[0].scrollHeight - chatBody.scrollTop() - parseInt(chatBody.css("padding-bottom"), 10) === chatBody.height()) {
                isBottom = true;
            }
            var newMessage = $('<div/>', { 'class': "message border border-secondary rounded", id: message.id })
                .append(
                    $('<h5/>', { text: message.author })).append(
                        $('<p/>', { text: message.body })).append(
                            $('<span/>', { 'class': 'time', text: message.creationDate }));

            chatBody.append(newMessage);

            var conversationId = $(".current-conversation").attr("id");
            $(".conversations").prepend($(".conversations").children("[id ='" + conversationId + "']"));
            if (isBottom) {
                chatBody.animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 200);
            }
        });

    $("#send-message").click(function () {
        var conversationId = $(".current-conversation").attr("id");
        var messageInput = $('.message-input input');
        if (conversationId.length > 0 && messageInput.val().trim().length > 0) {
            connection.invoke('SendMessage', conversationId, messageInput.val());
            messageInput.val("");
            $(".conversations").prepend($(".conversations").children("[id ='" + conversationId + "']"));
            $(".chatBody").animate({ scrollTop: $(".chat-body")[0].scrollHeight }, 200);
        }
    });

    connection.on("ConversationUp",
        (conversationId) => {
            $(".conversations").prepend($(".conversations").children("[id ='" + conversationId + "']"));
        });

    function validatePossibleUsers() {
        var possibleMembers = $(".possible-members");
        if (possibleMembers.children().length < 2) {

            if ($("#possible-users-error").length === 0) {

                $("<div class = 'text-danger' id='possible-users-error'> Необходимо добавить хотя бы двух пользователей</div>").insertBefore(possibleMembers);
            }
            return false;
        } else {
            $("#possible-users-error").remove();
            return true;
        }
    }

    $(".conversations").on('click',
        '.conversation',
        function () {
            var newConversationId = $(this).attr("id");
            var currentConversationId = $(".current-conversation").attr("id");
            if (newConversationId === currentConversationId) {
                return;
            }

            $(".chat-body").off('scroll', scrollHandler);
            $(".conversations .active").removeClass("active");
            $(this).addClass("active");
            connection.invoke('ChangeConversation', currentConversationId, newConversationId);
            $(".chat-body").empty();
            $(".current-conversation").attr("id", newConversationId);
            connection.invoke('GetConversationHistory', newConversationId, null);
            $(".members").empty();
            connection.invoke('GetMembers', newConversationId);
            var newConversationCreatorId = $(this).attr("data-creators-id");
            $(".current-conversation").attr("data-creators-id", newConversationCreatorId);
            $("#leave-conversation").prop('disabled', false);
            $(".add-member").prop('disabled', false);
            $("#send-message").prop('disabled', false);
            $("#message").prop('disabled', false);
            notShownMessagesExist = true;

            if ($(this).data('type') === "Private") {
                $("#leave-conversation").prop('disabled', true);
                $(".add-member").prop('disabled', true);
            }
            $('.current-conversation h5').remove();
            var newConversationName = $(this)
                .clone()    
                .children() 
                .remove()   
                .end()  
                .text();
            $('.current-conversation').append("<h5>" + newConversationName + "</h5>");
        });

    $(".chat-body").scroll(scrollHandler);

    function scrollHandler() {
        if ($(".chat-body").scrollTop() === 0 && !inProgress && notShownMessagesExist) {
            inProgress = true;
            var currentConversationId = $(".current-conversation").attr("id");
            var firstMessageId = $(".message").first().attr('id');
            connection.invoke('GetConversationHistory', currentConversationId, firstMessageId);
        }
    }

    connection.on("SetRead",
        function (newConversationId) {
            $(".conversation[id ='" + newConversationId + "']").children(".badge").remove();
        });

    connection.on('SetUserOnline', function (userId) {
        var member = $(".members [id = '" + userId + "']");
        member.children(".status").removeClass('offline').addClass('online').text("Online");
    });

    connection.on('SetUserOffline', function (userId) {
        var member = $(".members [id = '" + userId + "']");
        member.children(".status").removeClass('online').addClass('offline').text("Offline");
    });

    connection.on('NewUnreadMessage', function (conversationId) {
        var conversation = $('.conversation[id="' + conversationId + '"]');

        if (conversation.children('.badge').length === 0) {
            var currentConversationId = $(".current-conversation").attr("id");
            if (currentConversationId === conversationId && !document.hidden) {
                connection.invoke('SetMessageRead', $('.chat-body .message:last').attr('id'));
            } else {
                $(conversation).append('<span class="badge badge-secondary">1</span>');
            }
        } else {
            var currentUnreadMessagesCount = parseInt(conversation.children('.badge').text());
            conversation.children('.badge').text(currentUnreadMessagesCount + 1);
        }

        $(".conversations").prepend(conversation);
    });


    $(document).on('visibilitychange', function () {
        var currentConversationId = $(".current-conversation").attr("id");
        var unreadMessagesCount = $(".conversation[id ='" + currentConversationId + "']").children(".badge");
        if (unreadMessagesCount.length > 0) {
            connection.invoke('SetAllMessagesRead', currentConversationId);
        }
    });

    $('#create-group-modal').on('hidden.bs.modal', function () {
        $('#create-group-modal input').val('');
        $('#create-group-modal .text-danger').text('');
        $('#create-group-modal .possible-members').empty();
    });
    $('#add-member-modal').on('hidden.bs.modal', function () {
        $('#add-member-modal input').val('').removeClass('is-invalid');
        $('#add-member-modal .text-danger').remove();
    });
    $('#create-private-conversation-modal').on('hidden.bs.modal', function () {
        $('#create-private-conversation-modal input').val('').removeClass('is-invalid');
        $('#create-private-conversation-modal .text-danger').remove();
    });

    connection.start().catch(err => console.error(err.toString()));
})