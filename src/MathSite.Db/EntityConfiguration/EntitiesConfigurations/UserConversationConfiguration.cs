using MathSite.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class UserConversationConfiguration : AbstractEntityConfiguration<UserConversation>
    {
        protected override string TableName { get; } = nameof(UserConversation);

        protected override void SetRelationships(EntityTypeBuilder<UserConversation> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(userConversation => userConversation.User)
                .WithMany(user => user.UserConversations)
                .HasForeignKey(userConversation => userConversation.UserId);

            modelBuilder
                .HasOne(userConversation => userConversation.Conversation)
                .WithMany(conversation => conversation.UserConversations)
                .HasForeignKey(userConversation => userConversation.ConversationId);

            modelBuilder
                .HasMany(userConversation => userConversation.MessageUserConversations)
                .WithOne(messageUser => messageUser.UserConversation)
                .HasForeignKey(messageUser => messageUser.UserConversationId);
        }
    }

}
