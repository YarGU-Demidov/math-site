using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class ProfessorConfiguration : AbstractEntityConfiguration<Professor>
    {
        protected override string TableName { get; } = nameof(Professor);
        
        protected override void SetFields(EntityTypeBuilder<Professor> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder.Property(professor => professor.Description)
                .IsRequired();

            modelBuilder.Property(professor => professor.PersonId)
                .IsRequired();

            modelBuilder.Property(professor => professor.BibliographicIndexOfWorks)
                .IsRequired(false);

            modelBuilder.Property(professor => professor.Graduated)
                .IsRequired(false);

            modelBuilder.Property(professor => professor.MathNetLink)
                .IsRequired(false);

            modelBuilder.Property(professor => professor.ScientificTitle)
                .IsRequired(false);

            modelBuilder.Property(professor => professor.Status)
                .IsRequired();

            modelBuilder.Property(professor => professor.TermPapers)
                .IsRequired(false);

            modelBuilder.Property(professor => professor.Theses)
                .IsRequired(false);
        }

        protected override void SetRelationships(EntityTypeBuilder<Professor> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(professor => professor.Person)
                .WithOne(professor => professor.Professor)
                .HasForeignKey<Professor>(professor => professor.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void SetIndexes(EntityTypeBuilder<Professor> modelBuilder)
        {
            base.SetIndexes(modelBuilder);

            modelBuilder.HasIndex(professor => professor.PersonId)
                .IsUnique();
        }
    }
}