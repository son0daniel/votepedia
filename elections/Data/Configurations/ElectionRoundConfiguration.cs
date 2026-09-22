using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class ElectionRoundConfiguration : IEntityTypeConfiguration<ElectionRound>
    {
        public void Configure(EntityTypeBuilder<ElectionRound> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com os turnos de eleições."));
            builder.ToTable(t => t.HasCheckConstraint("CK_election_round", "nr_round IN (1, 2)"));

            builder.Property(x => x.Id).HasComment("ID interno do turno da eleição.");
            builder.Property(x => x.Uid).HasComment("ID externo do turno da eleição.");
            builder.Property(x => x.NrRound).HasComment("Número do turno da eleição.");
            builder.Property(x => x.HeldAt).HasComment("Data da realização do turno da eleição.");
            builder.Property(x => x.ElectionId).HasComment("ID interno da eleição.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_election_round_uid");
            builder.HasIndex(x => new { x.ElectionId, x.NrRound }).HasDatabaseName("IX_election_round_election_nr_round");

            builder.HasOne(x => x.Election)
                .WithMany(x => x.ElectionRounds)
                .HasForeignKey(x => x.ElectionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_election_round_election_id");
        }
    }
}
