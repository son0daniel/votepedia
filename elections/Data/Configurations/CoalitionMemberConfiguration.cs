using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class CoalitionMemberConfiguration : IEntityTypeConfiguration<CoalitionMember>
    {
        public void Configure(EntityTypeBuilder<CoalitionMember> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação que expressa o vínculo de um partido ou federação partidária a uma coligação"));
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_coalition_member_party_or_federation",
                """
                (party_id IS NOT NULL AND federation_id IS NULL)
                OR
                (party_id IS NULL AND federation_id IS NOT NULL)
                """));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.CoalitionId).HasComment("ID interno da coligação.");
            builder.Property(x => x.PartyId).HasComment("ID interno do partido político coligado.");
            builder.Property(x => x.FederationId).HasComment("ID interno da federação partidária coligada.");

            builder.HasIndex(x => new { x.CoalitionId, x.PartyId }).IsUnique().HasDatabaseName("IX_coalition_member_party");
            builder.HasIndex(x => new { x.CoalitionId, x.FederationId }).IsUnique().HasDatabaseName("IX_coalition_member_federation");

            builder.HasOne(x => x.Party)
                .WithMany(x => x.CoalitionMembers)
                .HasForeignKey(x => x.PartyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_coalition_member_party_id");

            builder.HasOne(x => x.Federation)
                .WithMany(x => x.CoalitionMembers)
                .HasForeignKey(x => x.FederationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_coalition_member_federation_id");
        }
    }
}
