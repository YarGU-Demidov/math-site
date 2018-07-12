using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class MessageConfiguration : AbstractEntityConfiguration<Message>
    {
        protected override string TableName { get; } = nameof(Message);

        protected override void SetRelationships(EntityTypeBuilder<Message> modelBuilder)
        {

            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(message => message.Author)
                .WithMany(user => user.Messages)
                .HasForeignKey(message => message.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(message => message.Conversation)
                .WithMany(conversation => conversation.Messages)
                .HasForeignKey(message => message.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(message => message.MessageUserConversations)
                .WithOne(messageUserConversation => messageUserConversation.Message)
                .HasForeignKey(messageUserConversation => messageUserConversation.MessageId);
        }

    }
}
