using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket")]
    public class Ticket : BaseEntityWithUid
    {
        [Column("description")]
        public string? Description { get; set; }

        [Column("ticket_color_id")]
        public long? TicketColorId { get; set; }

        public TicketColor? TicketColor { get; set; }

        public Coalition? Coalition { get; set; }

        public ICollection<TicketCandidateParty> TicketCandidateParties { get; set; } = [];

        public ICollection<TicketElectionRound> TicketElectionRounds { get; set; } = [];

        public ICollection<TicketElectionRoundStatistic> TicketElectionRoundStatistics { get; set; } = [];
    }
}
