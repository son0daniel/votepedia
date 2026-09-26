using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket_election_round")]
    public class TicketElectionRound : BaseEntity
    {
        [Column("ticket_id")]
        [Required]
        public long TicketId { get; set; }

        [Column("election_round_id")]
        [Required]
        public long ElectionRoundId { get; set; }

        public Ticket Ticket { get; set; } = default!;

        public ElectionRound ElectionRound { get; set; } = default!;
    }
}
