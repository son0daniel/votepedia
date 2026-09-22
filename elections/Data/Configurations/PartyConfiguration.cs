using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com os partidos políticos. Pode ter registros do mesmo partido se ele mudou de nome ou se fundiu-se ou incorporou-se a outro partido."));

            builder.Property(x => x.Id).HasComment("ID interno do partido.");
            builder.Property(x => x.Uid).HasComment("ID externo do partido.");
            builder.Property(x => x.Name).HasComment("Nome completo do partido.");
            builder.Property(x => x.Abbr).HasMaxLength(50).HasComment("Sigla do partido.");
            builder.Property(x => x.NrVoteable).HasComment("Número do partido.");
            builder.Property(x => x.IsActive).HasComment("Campo booleano que determina se o partido está ativo ou não.");
            builder.Property(x => x.FoundedAt).HasComment("Data de fundação do partido.");
            builder.Property(x => x.RegisteredAt).HasComment("Data de registro do partido junto ao TSE.");
            builder.Property(x => x.DissolvedAt).HasComment("Data de dissolução/mudança de nome/fusão/incorporação do partido.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_party_uid");
        }
    }
}
