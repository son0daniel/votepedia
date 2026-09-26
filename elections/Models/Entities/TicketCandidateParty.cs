using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket_candidate_party")]
    public class TicketCandidateParty : BaseEntity
    {
        [Column("nr_level")]
        [Required]
        public int NrLevel { get; set; }

        [Column("ticket_id")]
        [Required]
        public long TicketId { get; set; }

        [Column("candidate_party_id")]
        [Required]
        public long CandidatePartyId { get; set; }

        public Ticket Ticket { get; set; } = default!;

        public CandidateParty CandidateParty { get; set; } = default!;
    }
}
