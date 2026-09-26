using elections.Constants;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as chapas eleitorais."));
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_ticket_color",
                 $"""
                 color IS NULL 
                 OR color IN ({string.Join(", ", Constant.Color.All.Select(x => $"'{x}'"))})
                 """));

            builder.Property(x => x.Id).HasComment("ID interno da chapa.");
            builder.Property(x => x.Uid).HasComment("ID externo da chapa.");
            builder.Property(x => x.Color).HasMaxLength(50).HasComment("Cor usada para representar a chapa em mapas eleitorais.");
            builder.Property(x => x.Description).HasMaxLength(50).HasComment("Campo opcional usado para contextualizar o registro da chapa eleitoral.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_ticket_uid");;
        }
    }
}
