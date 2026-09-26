using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("coalition")]
    public class Coalition : BaseEntityWithUid
    {
        [Column("name")]
        public string? Name { get; set; }

        [Column("ticket_id")]
        [Required]
        public long TicketId { get; set; }

        public Ticket Ticket { get; set; } = default!;
    }
}
