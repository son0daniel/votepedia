using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class FederationConfiguration : IEntityTypeConfiguration<Federation>
    {
        public void Configure(EntityTypeBuilder<Federation> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as federações partidárias, instituto criado pela Lei nº 14.208/2021"));
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_federation_date", 
                "dissolved_at IS NULL OR registered_at < dissolved_at"));

            builder.Property(x => x.Id).HasComment("ID interno da federação partidária.");
            builder.Property(x => x.Uid).HasComment("ID externo da federação partidária.");
            builder.Property(x => x.Name).HasMaxLength(50).HasComment("Nome formal da federação partidária.");
            builder.Property(x => x.DisplayName).HasMaxLength(50).HasComment("Nome de exibição da federação partidária.");
            builder.Property(x => x.RegisteredAt).HasComment("Data de registro da federação partidária.");
            builder.Property(x => x.DissolvedAt).HasComment("Data de dissolução da federação partidária.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_federation_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_federation_name");
            builder.HasIndex(x => x.DisplayName).IsUnique().HasDatabaseName("IX_federation_display_name");
        }
    }
}
