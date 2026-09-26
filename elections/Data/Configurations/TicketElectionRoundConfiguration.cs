using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class TicketElectionRoundConfiguration : IEntityTypeConfiguration<TicketElectionRound>
    {
        public void Configure(EntityTypeBuilder<TicketElectionRound> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação que expressa a participação de determinada chapa eleitoral em determinado turno de uma eleição. Útil para ingestão de votos usando arquivos de resultados csv do TSE."));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.TicketId).HasComment("ID interno da chapa eleitoral.");
            builder.Property(x => x.ElectionRoundId).HasComment("ID interno do turno da eleição.");

            builder.HasIndex(x => new { x.TicketId, x.ElectionRoundId }).IsUnique().HasDatabaseName("IX_ticket_election_round");

            builder.HasOne(x => x.Ticket)
                .WithMany(x => x.TicketElectionRounds)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_ticket_id");

            builder.HasOne(x => x.ElectionRound)
                .WithMany(x => x.TicketElectionRounds)
                .HasForeignKey(x => x.ElectionRoundId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_election_round_id");
        }
    }
}
