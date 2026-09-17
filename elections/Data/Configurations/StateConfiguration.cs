using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable(t => t
                .HasComment("Tabela com 27 UFs do Brasil + 1 registro indicando Exterior.")
            );
            builder.ToTable(t => t
                .HasCheckConstraint("CK_state_abbr", "abbr REGEXP '^[A-Z]{2}$'")
            );

            builder.Property(x => x.Id).HasComment("ID interno da UF.");
            builder.Property(x => x.Uid).HasComment("ID externo da UF.");
            builder.Property(x => x.Name).HasComment("Nome da UF.");
            builder.Property(x => x.Abbr).HasMaxLength(2).HasComment("Sigla da UF.");
            builder.Property(x => x.MacroregionId).HasComment("ID interno da macroregião na qual a UF está localizada.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_state_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_state_name");
            builder.HasIndex(x => x.Abbr).IsUnique().HasDatabaseName("IX_state_abbr");

            builder.HasOne(x => x.Macroregion)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.MacroregionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_state_macroregion");
        }
    }
}
