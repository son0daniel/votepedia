using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket_color")]
    public class TicketColor : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("standard_hex")]
        [Required]
        public string StandardHex { get; set; } = string.Empty;

        [Column("lightest_hex")]
        [Required]
        public string LightestHex { get; set; } = string.Empty;

        [Column("darkest_hex")]
        [Required]
        public string DarkestHex { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; } = [];
    }
}
