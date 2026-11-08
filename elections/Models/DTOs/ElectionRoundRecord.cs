using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class ElectionRoundRecord(int year, int roundNr, Coalition? coalition, TicketElectionRoundStatistic ticketElectionRoundStatistic) : CandidacyRecord(ticketElectionRoundStatistic)
    {
        public int Year { get; } = year;
        public int RoundNr { get; } = roundNr;
        public string? Coalition { get; } = coalition?.Name;
        [JsonPropertyName("has_coalition")] public bool HasCoalition => coalition is not null;
        public string Status { get; } = ticketElectionRoundStatistic.TicketStatus;
        [JsonPropertyName("position_nr")] public int PositionNr { get; } = ticketElectionRoundStatistic.TicketPositionNr;
    }
}
