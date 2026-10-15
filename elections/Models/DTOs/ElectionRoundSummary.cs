using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class ElectionRoundSummary(TicketElectionRoundStatistic ticketElectionRoundStatistic)
    {
        [JsonPropertyName("registered_voters_count")] public int RegisteredVotersCount { get; } = ticketElectionRoundStatistic.RegisteredVotersCount;
        [JsonPropertyName("turnout_count")] public int TurnoutCount { get; } = ticketElectionRoundStatistic.TurnoutCount;
        [JsonPropertyName("turnout_pp")] public float TurnoutPp { get; } = ticketElectionRoundStatistic.TurnoutPp;
        [JsonPropertyName("abstention_count")] public int AbstentionCount { get; } = ticketElectionRoundStatistic.AbstentionCount;
        [JsonPropertyName("abstention_pp")] public float AbstentionPp { get; } = ticketElectionRoundStatistic.AbstentionPp;
        [JsonPropertyName("electoral_alienation_count")] public int ElectoralAlienationCount { get; } = ticketElectionRoundStatistic.ElectoralAlienationCount;
        [JsonPropertyName("electoral_alienation_pp")] public float ElectoralAlienationPp { get; } = ticketElectionRoundStatistic.ElectoralAlienationPp;
        [JsonPropertyName("valid_votes_count")] public int ValidVotesCount { get; } = ticketElectionRoundStatistic.ValidVotesCount;
        [JsonPropertyName("valid_votes_pp")] public float? ValidVotesPp { get; } = ticketElectionRoundStatistic.ValidVotesPp;
        [JsonPropertyName("valid_votes_registered_pp")] public float ValidVotesRegisteredPp { get; } = ticketElectionRoundStatistic.ValidVotesRegisteredPp;
        [JsonPropertyName("invalid_votes_count")] public int InvalidVotesCount { get; } = ticketElectionRoundStatistic.InvalidVotesCount;
        [JsonPropertyName("invalid_votes_pp")] public float? InvalidVotesPp { get; } = ticketElectionRoundStatistic.InvalidVotesPp;
        [JsonPropertyName("invalid_votes_registered_pp")] public float InvalidVotesRegisteredPp { get; } = ticketElectionRoundStatistic.InvalidVotesRegisteredPp;
        [JsonPropertyName("null_votes_count")] public int NullVotesCount { get; } = ticketElectionRoundStatistic.NullVotesCount;
        [JsonPropertyName("null_votes_pp")] public float? NullVotesPp { get; } = ticketElectionRoundStatistic.NullVotesPp;
        [JsonPropertyName("null_votes_registered_pp")] public float NullVotesRegisteredPp { get; } = ticketElectionRoundStatistic.NullVotesRegisteredPp;
        [JsonPropertyName("blank_votes_count")] public int BlankVotesCount { get; } = ticketElectionRoundStatistic.BlankVotesCount;
        [JsonPropertyName("blank_votes_pp")] public float? BlankVotesPp { get; } = ticketElectionRoundStatistic.BlankVotesPp;
        [JsonPropertyName("blank_votes_registered_pp")] public float BlankVotesRegisteredPp { get; } = ticketElectionRoundStatistic.BlankVotesRegisteredPp;
        [JsonPropertyName("anulled_votes_count")] public int AnulledVotesCount { get; } = ticketElectionRoundStatistic.AnulledVotesCount;
        [JsonPropertyName("anulled_votes_pp")] public float? AnulledVotesPp { get; } = ticketElectionRoundStatistic.AnulledVotesPp;
        [JsonPropertyName("anulled_votes_registered_pp")] public float AnulledVotesRegisteredPp { get; } = ticketElectionRoundStatistic.AnulledVotesRegisteredPp;
    }
}
