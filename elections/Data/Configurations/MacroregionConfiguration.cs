using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class MacroregionConfiguration : IEntityTypeConfiguration<Macroregion>
    {
        public void Configure(EntityTypeBuilder<Macroregion> builder)
        {
            builder.ToTable(t => t
                .HasComment("Tabela com as 5 macrorregiões brasileiras + 1 registro do Exterior.")
            );
            builder.ToTable(t => t
                .HasCheckConstraint("CK_macroregion_abbr", "abbr REGEXP '^[A-Z]{1,2}$'")
            );

            builder.Property(x => x.Id).HasComment("ID interno da macrorregião.");
            builder.Property(x => x.Uid).HasComment("ID externo da macrorregião.");
            builder.Property(x => x.Name).HasMaxLength(100).HasComment("Nome da macrorregião.");
            builder.Property(x => x.Abbr).HasMaxLength(2).HasComment("Sigla da macrorregião.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_macroregion_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_macroregion_name");
            builder.HasIndex(x => x.Abbr).IsUnique().HasDatabaseName("IX_macroregion_abbr");
        }
    }
}
