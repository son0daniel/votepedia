using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs.ElectionOverviews
{
    public abstract class BaseElectionOverview
    {
        public string Id { get; set; } = string.Empty;
        public int Year { get; set; }
        [JsonPropertyName("round_nr")] public int RoundNr { get; set; }
        [JsonPropertyName("ticket_votes_count")] public int TicketVotesCount { get; set; }
        [JsonPropertyName("ticket_valid_votes_pp")] public float? TicketValidVotesPp { get; set; }
        [JsonPropertyName("ticket_total_votes_pp")] public float? TicketTotalVotesPp { get; set; }
        [JsonPropertyName("ticket_registered_votes_pp")] public float TicketRegisteredVotesPp { get; set; }
        [JsonPropertyName("registered_voters_count")] public int RegisteredVotersCount { get; set; }
        [JsonPropertyName("turnout_count")] public int TurnoutCount { get; set; }
        [JsonPropertyName("turnout_pp")] public float TurnoutPp { get; set; }
        [JsonPropertyName("abstention_count")] public int AbstentionCount { get; set; }
        [JsonPropertyName("abstention_pp")] public float AbstentionPp { get; set; }
        [JsonPropertyName("electoral_alienation_count")] public int ElectoralAlienationCount { get; set; }
        [JsonPropertyName("electoral_alienation_pp")] public float ElectoralAlienationPp { get; set; }
        [JsonPropertyName("valid_votes_count")] public int ValidVotesCount { get; set; }
        [JsonPropertyName("valid_votes_pp")] public float? ValidVotesPp { get; set; }
        [JsonPropertyName("valid_votes_registered_pp")] public float ValidVotesRegisteredPp { get; set; }
        [JsonPropertyName("invalid_votes_count")] public int InvalidVotesCount { get; set; }
        [JsonPropertyName("invalid_votes_pp")] public float? InvalidVotesPp { get; set; }
        [JsonPropertyName("invalid_votes_registered_pp")] public float InvalidVotesRegisteredPp { get; set; }
        [JsonPropertyName("null_votes_count")] public int NullVotesCount { get; set; }
        [JsonPropertyName("null_votes_pp")] public float? NullVotesPp { get; set; }
        [JsonPropertyName("null_votes_registered_pp")] public float NullVotesRegisteredPp { get; set; }
        [JsonPropertyName("blank_votes_count")] public int BlankVotesCount { get; set; }
        [JsonPropertyName("blank_votes_pp")] public float? BlankVotesPp { get; set; }
        [JsonPropertyName("blank_votes_registered_pp")] public float BlankVotesRegisteredPp { get; set; }
        [JsonPropertyName("anulled_votes_count")] public int AnulledVotesCount { get; set; }
        [JsonPropertyName("anulled_votes_pp")] public float? AnulledVotesPp { get; set; }
        [JsonPropertyName("anulled_votes_registered_pp")] public float AnulledVotesRegisteredPp { get; set; }

        protected static TOverview New<TOverview>(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic) where TOverview : BaseElectionOverview, new()
        {
            return new TOverview
            {
                Id = election.Uid,
                Year = election.Year,
                RoundNr = electionRound.NrRound,
                TicketVotesCount = ticketElectionRoundStatistic.TicketVotesCount,
                TicketValidVotesPp = ticketElectionRoundStatistic.TicketValidVotesPp,
                TicketTotalVotesPp = ticketElectionRoundStatistic.TicketTotalVotesPp,
                TicketRegisteredVotesPp = ticketElectionRoundStatistic.TicketRegisteredVotesPp,
                RegisteredVotersCount = ticketElectionRoundStatistic.RegisteredVotersCount,
                TurnoutCount = ticketElectionRoundStatistic.TurnoutCount,
                TurnoutPp = ticketElectionRoundStatistic.TurnoutPp,
                AbstentionCount = ticketElectionRoundStatistic.AbstentionCount,
                AbstentionPp = ticketElectionRoundStatistic.AbstentionPp,
                ElectoralAlienationCount = ticketElectionRoundStatistic.ElectoralAlienationCount,
                ElectoralAlienationPp = ticketElectionRoundStatistic.ElectoralAlienationPp,
                ValidVotesCount = ticketElectionRoundStatistic.ValidVotesCount,
                ValidVotesPp = ticketElectionRoundStatistic.ValidVotesPp,
                ValidVotesRegisteredPp = ticketElectionRoundStatistic.ValidVotesRegisteredPp,
                InvalidVotesCount = ticketElectionRoundStatistic.InvalidVotesCount,
                InvalidVotesPp = ticketElectionRoundStatistic.InvalidVotesPp,
                InvalidVotesRegisteredPp = ticketElectionRoundStatistic.InvalidVotesRegisteredPp,
                NullVotesCount = ticketElectionRoundStatistic.NullVotesCount,
                NullVotesPp = ticketElectionRoundStatistic.NullVotesPp,
                NullVotesRegisteredPp = ticketElectionRoundStatistic.NullVotesRegisteredPp,
                BlankVotesCount = ticketElectionRoundStatistic.BlankVotesCount,
                BlankVotesPp = ticketElectionRoundStatistic.BlankVotesPp,
                BlankVotesRegisteredPp = ticketElectionRoundStatistic.BlankVotesRegisteredPp,
                AnulledVotesCount = ticketElectionRoundStatistic.AnulledVotesCount,
                AnulledVotesPp = ticketElectionRoundStatistic.AnulledVotesPp,
                AnulledVotesRegisteredPp = ticketElectionRoundStatistic.AnulledVotesRegisteredPp,
            };
        }
    }
}
