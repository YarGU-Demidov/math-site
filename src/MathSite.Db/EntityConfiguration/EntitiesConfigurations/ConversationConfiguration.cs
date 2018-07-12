using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class ConversationConfiguration : AbstractEntityConfiguration<Conversation>

    {
        protected override string TableName { get; } = nameof(Conversation);


        protected override void SetRelationships(EntityTypeBuilder<Conversation> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(conversation => conversation.UserConversations)
                .WithOne(userConversations => userConversations.Conversation)
                .HasForeignKey(userConversations => userConversations.ConversationId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder
                .HasMany(conversation => conversation.Messages)
                .WithOne(message => message.Conversation)
                .HasForeignKey(message => message.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(convesation => convesation.Creator)
                .WithMany(user => user.ConversationsCreated)
                .HasForeignKey(conversation => conversation.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
