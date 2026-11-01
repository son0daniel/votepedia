using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MunicipalityElectionRoundDto(List<MinimalTicketDto> tickets, Municipality municipality, string stateAbbr, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic)
    {
        public List<MinimalTicketDto> Tickets { get; } = tickets;
        public string Municipality { get; } = municipality.Name;
        [JsonPropertyName("ibge_id")] public long? IbgeId { get; } = municipality.IbgeId;
        [JsonPropertyName("tse_id")] public long TseId { get; } = municipality.TseId;
        public string State { get; } = stateAbbr;
    }
}
