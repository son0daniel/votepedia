using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class CandidatePartyConfiguration : IEntityTypeConfiguration<CandidateParty>
    {
        public void Configure(EntityTypeBuilder<CandidateParty> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação entre um candidato e um partido político — a filiação de um político a determinado partido."));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.CandidateId).HasComment("ID interno do candidato.");
            builder.Property(x => x.PartyId).HasComment("ID interno do partido.");

            builder.HasIndex(x => new { x.CandidateId, x.PartyId }).IsUnique().HasDatabaseName("IX_candidate_party");

            builder.HasOne(x => x.Candidate)
                .WithMany(x => x.CandidateParties)
                .HasForeignKey(x => x.CandidateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_candidate_party_candidate");

            builder.HasOne(x => x.Party)
                .WithMany(x => x.CandidateParties)
                .HasForeignKey(x => x.PartyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_candidate_party_party");
        }
    }
}
