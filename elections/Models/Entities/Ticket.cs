using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket")]
    public class Ticket : BaseEntityWithUid
    {
        [Column("color")]
        public string? Color { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        public Coalition? Coalition { get; set; }

        public ICollection<TicketCandidateParty> TicketCandidateParties { get; set; } = [];

        public ICollection<TicketElectionRound> TicketElectionRounds { get; set; } = [];
    }
}
