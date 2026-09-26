using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class TicketCandidatePartyConfiguration : IEntityTypeConfiguration<TicketCandidateParty>
    {
        public void Configure(EntityTypeBuilder<TicketCandidateParty> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação entre a chapa eleitoral e o candidato com filiação partidária."));
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_ticket_candidate_party_nr_level", "nr_level IN (1, 2, 3)"));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.NrLevel).HasComment("A hierarquia do candidato em relação à chapa. 1 é titular; 2 é vice/primeiro suplente; e 3 para segundo suplente.");
            builder.Property(x => x.TicketId).HasComment("ID interno da chapa eleitoral.");
            builder.Property(x => x.CandidatePartyId).HasComment("ID interno da filiação partidária do candidato.");

            builder.HasIndex(x => new { x.TicketId, x.CandidatePartyId }).IsUnique().HasDatabaseName("IX_ticket_candidate_party");
            builder.HasIndex(x => new { x.TicketId, x.NrLevel }).IsUnique().HasDatabaseName("IX_ticket_candidate_party_nr_level");

            builder.HasOne(x => x.Ticket)
                .WithMany(x => x.TicketCandidateParties)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_candidate_party_ticket_id");

            builder.HasOne(x => x.CandidateParty)
                .WithMany(x => x.TicketCandidateParties)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_candidate_party_candidate_party_id");
        }
    }
}
