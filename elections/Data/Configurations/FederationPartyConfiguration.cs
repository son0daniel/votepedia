using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class FederationPartyConfiguration : IEntityTypeConfiguration<FederationParty>
    {
        public void Configure(EntityTypeBuilder<FederationParty> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação entre a federação partidária e o partido político."));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.FederationId).HasComment("ID interno da federação partidária.");
            builder.Property(x => x.PartyId).HasComment("ID interno do partido político.");

            builder.HasIndex(x => new { x.FederationId, x.PartyId }).IsUnique().HasDatabaseName("IX_federation_party");

            builder.HasOne(x => x.Federation)
                .WithMany(x => x.FederationParties)
                .HasForeignKey(x => x.FederationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_federation_party_federation_id");

            builder.HasOne(x => x.Party)
                .WithMany(x => x.FederationParties)
                .HasForeignKey(x => x.PartyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_federation_party_party_id");
        }
    }
}
