using elections.Commons.Extensions;
using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class ElectionRoundDto(List<TicketDto> tickets, DateOnly heldAt, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic)
    {
        public List<TicketDto> Tickets { get; } = tickets;
        [JsonPropertyName("held_at")] public long HeldAt { get; } = heldAt.ToUnixTimestamp();
    }
}
