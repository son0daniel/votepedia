using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.ToTable(t => t
                .HasComment("Tabela com os municípios brasileiros ou cidades estrangeiras com votações de eleições brasilieiras. Pode aceitar duplicatas de cidades do exterior porque, nesses casos, o TSE dá um código diferente a cada seção eleitoral, ainda que da mesma localidade.")
            );

            builder.Property(x => x.Id).HasComment("ID interno do município.");
            builder.Property(x => x.Uid).HasComment("ID externo do município.");
            builder.Property(x => x.Name).IsRequired().HasComment("Nome do município.");
            builder.Property(x => x.IsCapital).HasComment("Determina se o município é a capital do Estado. Válido apenas para cidades brasileiras.");
            builder.Property(x => x.TseId).HasComment("Código do TSE ligado ao município que pode ser visto nos arquivos de resultados.");
            builder.Property(x => x.IbgeId).HasComment("ID do município no IBGE.");
            builder.Property(x => x.StateId).HasComment("ID interno da UF na qual o município está localizado.");
            builder.Property(x => x.MetropolitanAreaId).HasComment("ID interno da região metropolitana na qual o município pode estar localizado.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_municipality_uid");
            builder.HasIndex(x => x.TseId).IsUnique().HasDatabaseName("IX_municipality_tse_id");
            builder.HasIndex(x => x.IbgeId).IsUnique().HasDatabaseName("IX_municipality_ibge_id");

            builder.HasOne(x => x.State)
                .WithMany(x => x.Municipalities)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_municipality_state");

            builder.HasOne(x => x.MetropolitanArea)
                .WithMany(x => x.Municipalities)
                .HasForeignKey(x => x.MetropolitanAreaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_municipality_metropolitan_area");

        }
    }
}
