using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class TicketDto(string ticketUid, string? coalition, bool hasCoalition, TicketElectionRoundStatistic ticketElectionRoundStatistic, List<TicketCandidatePartyDto> members)
    {
        public string Id { get; } = ticketUid;
        public string? Coalition { get; } = coalition;
        [JsonPropertyName("has_coalition")] public bool HasCoaltion { get; } = hasCoalition;
        public List<TicketCandidatePartyDto> Members { get; } = members;
        [JsonPropertyName("votes_count")] public int VotesCount { get; } = ticketElectionRoundStatistic.TicketVotesCount;
        [JsonPropertyName("valid_votes_pp")] public float? ValidVotesPp { get; } = ticketElectionRoundStatistic.TicketValidVotesPp;
        [JsonPropertyName("total_votes_pp")] public float? TotalVotesPp { get; } = ticketElectionRoundStatistic.TicketTotalVotesPp;
        [JsonPropertyName("registered_votes_pp")] public float RegisteredVotesPp { get; } = ticketElectionRoundStatistic.TicketRegisteredVotesPp;
        public string Status { get; } = ticketElectionRoundStatistic.TicketStatus;
    }
}
