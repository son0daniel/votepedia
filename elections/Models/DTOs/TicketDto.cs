using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class TicketDto(string ticketUid, string? coalition, bool hasCoalition, TicketElectionRoundStatistic ticketElectionRoundStatistic, List<TicketCandidatePartyDto> members) : MinimalTicketDto(ticketUid, ticketElectionRoundStatistic)
    {
        public string? Coalition { get; } = coalition;
        [JsonPropertyName("has_coalition")] public bool HasCoaltion { get; } = hasCoalition;
        public List<TicketCandidatePartyDto> Members { get; } = members;
        public string Status { get; } = ticketElectionRoundStatistic.TicketStatus;
    }
}
