using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class MetropolitanAreaStateConfiguration : IEntityTypeConfiguration<MetropolitanAreaState>
    {
        public void Configure(EntityTypeBuilder<MetropolitanAreaState> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela de relação entre Regiões Metropolitanas e Estados."));

            builder.Property(x => x.Id).HasComment("ID interno da relação.");
            builder.Property(x => x.MetropolitanAreaId).HasComment("ID interno da Região Metropolitana.");
            builder.Property(x => x.StateId).HasComment("ID interno do Estado.");

            builder.HasOne(x => x.MetropolitanArea)
                .WithMany(x => x.MetropolitanAreaStates)
                .HasForeignKey(x => x.MetropolitanAreaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_metropolitan_area_state_metropolitan_area_id");

            builder.HasOne(x => x.State)
                .WithMany(x => x.MetropolitanAreaStates)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_metropolitan_area_state_state_id");
        }
    }
}
