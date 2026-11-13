using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class TicketColorConfiguration : IEntityTypeConfiguration<TicketColor>
    {
        public void Configure(EntityTypeBuilder<TicketColor> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as cores em hexadecimal utilizadas nos mapas eleitorais."));

            builder.Property(x => x.Id).HasComment("ID interno da cor.");
            builder.Property(x => x.Uid).HasComment("ID externo da cor.");
            builder.Property(x => x.Name).HasComment("Nome da cor.");
            builder.Property(x => x.StandardHex).HasMaxLength(6).IsFixedLength().HasComment("Código hexadecimal padrão da cor.");
            builder.Property(x => x.LightestHex).HasMaxLength(6).IsFixedLength().HasComment("Código hexadecimal do tom mais claro da cor exibido em mapas eleitorais.");
            builder.Property(x => x.DarkestHex).HasMaxLength(6).IsFixedLength().HasComment("Código hexadecimal do tom mais escuro da cor exibido em mapas eleitorais.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_ticket_color_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_ticket_color_name");
        }
    }
}
