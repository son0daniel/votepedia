using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class CandidacyRecord(TicketElectionRoundStatistic ticketElectionRoundStatistic)
    {
        [JsonPropertyName("votes_count")] public int VotesCount { get; } = ticketElectionRoundStatistic.TicketVotesCount;
        [JsonPropertyName("valid_votes_pp")] public float? ValidVotesPp { get; } = ticketElectionRoundStatistic.TicketValidVotesPp;
        [JsonPropertyName("total_votes_pp")] public float? TotalVotesPp { get; } = ticketElectionRoundStatistic.TicketTotalVotesPp;
        [JsonPropertyName("registered_votes_pp")] public float RegisteredVotesPp { get; } = ticketElectionRoundStatistic.TicketRegisteredVotesPp;
    }
}
