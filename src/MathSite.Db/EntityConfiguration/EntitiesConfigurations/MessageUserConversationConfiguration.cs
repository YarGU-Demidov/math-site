using System;
using System.Collections.Generic;
using System.Text;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class MessageUserConfiguration: AbstractEntityConfiguration<MessageUserConversation>
    {
        protected override string TableName { get; } = nameof(MessageUserConversation);

        protected override void SetRelationships(EntityTypeBuilder<MessageUserConversation> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(messageUser => messageUser.Message)
                .WithMany(message => message.MessageUserConversations)
                .HasForeignKey(messageUser => messageUser.MessageId);

            modelBuilder
                .HasOne(messageUser => messageUser.UserConversation)
                .WithMany(user => user.MessageUserConversations)
                .HasForeignKey(messageUser => messageUser.UserConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
