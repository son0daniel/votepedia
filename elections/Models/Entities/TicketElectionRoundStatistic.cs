using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("ticket_election_round_statistic")]
    public class TicketElectionRoundStatistic : BaseEntity
    {
        [Column("ticket_id")]
        [Required]
        public long TicketId { get; set; }

        [Column("election_round_id")]
        [Required]
        public long ElectionRoundId { get; set; }

        [Column("municipality_id")]
        public long? MunicipalityId { get; set; }

        [Column("metropolitan_area_id")]
        public long? MetropolitanAreaId { get; set; }

        [Column("state_id")]
        public long? StateId { get; set; }

        [Column("macroregion_id")]
        public long? MacroregionId { get; set; }

        [Column("ticket_votes_count")]
        [Required]
        public int TicketVotesCount { get; set; }

        [Column("ticket_position_nr")]
        [Required]
        public int TicketPositionNr { get; set; }

        [Column("ticket_valid_votes_pp")]
        public float? TicketValidVotesPp { get; set; }

        [Column("ticket_total_votes_pp")]
        public float? TicketTotalVotesPp { get; set; }

        [Column("ticket_registered_votes_pp")]
        [Required]
        public float TicketRegisteredVotesPp { get; set; }

        [Column("ticket_status")]
        [Required]
        public string TicketStatus = string.Empty;

        [Column("registered_voters_count")]
        [Required]
        public int RegisteredVotersCount { get; set; }

        [Column("turnout_count")]
        [Required]
        public int TurnoutCount { get; set; }

        [Column("abstention_count")]
        [Required]
        public int AbstentionCount { get; set; }

        [Column("valid_votes_count")]
        [Required]
        public int ValidVotesCount { get; set; }

        [Column("invalid_votes_count")]
        [Required]
        public int InvalidVotesCount { get; set; }

        [Column("null_votes_count")]
        [Required]
        public int NullVotesCount { get; set; }

        [Column("blank_votes_count")]
        [Required]
        public int BlankVotesCount { get; set; }

        [Column("anulled_votes_count")]
        [Required]
        public int AnulledVotesCount { get; set; }

        [Column("electoral_alienation_count")]
        [Required]
        public int ElectoralAlienationCount { get; set; }

        [Column("candidates_count")]
        [Required]
        public int CandidatesCount { get; set; }

        [Column("turnout_pp")]
        [Required]
        public float TurnoutPp { get; set; }

        [Column("abstention_pp")]
        [Required]
        public float AbstentionPp { get; set; }

        [Column("valid_votes_pp")]
        public float? ValidVotesPp { get; set; }

        [Column("valid_votes_registered_pp")]
        [Required]
        public float ValidVotesRegisteredPp { get; set; }

        [Column("invalid_votes_pp")]
        public float? InvalidVotesPp { get; set; }

        [Column("invalid_votes_registered_pp")]
        [Required]
        public float InvalidVotesRegisteredPp { get; set; }

        [Column("null_votes_pp")]
        public float? NullVotesPp { get; set; }

        [Column("null_votes_registered_pp")]
        [Required]
        public float NullVotesRegisteredPp { get; set; }

        [Column("blank_votes_pp")]
        public float? BlankVotesPp { get; set; }

        [Column("blank_votes_registered_pp")]
        [Required]
        public float BlankVotesRegisteredPp { get; set; }

        [Column("anulled_votes_pp")]
        public float? AnulledVotesPp { get; set; }

        [Column("anulled_votes_registered_pp")]
        [Required]
        public float AnulledVotesRegisteredPp { get; set; }

        [Column("electoral_alienation_pp")]
        [Required]
        public float ElectoralAlienationPp { get; set; }

        public Ticket Ticket { get; set; } = default!;

        public ElectionRound ElectionRound { get; set; } = default!;

        public Municipality? Municipality { get; set; }

        public MetropolitanArea? MetropolitanArea { get; set; }

        public State? State { get; set; }

        public Macroregion? Macroregion { get; set; }
    }
}
