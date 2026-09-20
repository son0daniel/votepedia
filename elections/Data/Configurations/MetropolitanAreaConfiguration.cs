using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using elections.Constants;

namespace elections.Data.Configurations
{
    public class MetropolitanAreaConfiguration : IEntityTypeConfiguration<MetropolitanArea>
    {
        public void Configure(EntityTypeBuilder<MetropolitanArea> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com regiões metropolitanas e regiões integradas de desenvolvimento"));
            builder.ToTable(t => t.HasCheckConstraint("CK_metropolitan_area_type", $"type IN ('{Constant.MetropolitanAreaType.Standard}', '{Constant.MetropolitanAreaType.IntegratedAreaDevelopment}')"));

            builder.Property(x => x.Id).HasComment("ID interno da região metropolitana.");
            builder.Property(x => x.Uid).HasComment("ID externo da região metropolitana.");
            builder.Property(x => x.Name).HasMaxLength(100).HasComment("Nome da região metropolitana.");
            builder.Property(x => x.Type).HasMaxLength(50).HasComment("Tipo da região metropolitana.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_metropolitan_area_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_metropolitan_area_name");
        }
    }
}
