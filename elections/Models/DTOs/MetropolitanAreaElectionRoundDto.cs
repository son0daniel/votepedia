using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MetropolitanAreaElectionRoundDto(List<MinimalTicketDto> tickets, MetropolitanArea metropolitanArea, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic) 
    {
        public string Id { get; } = metropolitanArea.Uid;
        [JsonPropertyName("metropolitan_area")] public string MetropolitanArea { get; } = metropolitanArea.Name;
        public List<MinimalTicketDto> Tickets { get; } = tickets;
    }
}
