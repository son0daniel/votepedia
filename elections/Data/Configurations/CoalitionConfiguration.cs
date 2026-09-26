using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class CoalitionConfiguration : IEntityTypeConfiguration<Coalition>
    {
        public void Configure(EntityTypeBuilder<Coalition> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as coligações."));

            builder.Property(x => x.Id).HasComment("ID interno da coligação.");
            builder.Property(x => x.Uid).HasComment("ID externo da coligação.");
            builder.Property(x => x.Name).HasComment("Nome da coligação.");
            builder.Property(x => x.TicketId).HasComment("ID interno da chapa eleitoral à qual a coligação está vinculada.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_coalition_uid");

            builder.HasOne(x => x.Ticket)
                .WithOne(x => x.Coalition)
                .HasForeignKey<Coalition>(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_coalition_ticket_id");
        }
    }
}
