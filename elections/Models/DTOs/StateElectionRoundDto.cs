using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class StateElectionRoundDto(List<MinimalTicketDto> tickets, State state, string macroregionAbbr, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic)
    {
        public string State { get; } = state.Name;
        public string Abbr { get; } = state.Abbr;
        public string Macroregion { get; } = macroregionAbbr;
        public List<MinimalTicketDto> Tickets { get; } = tickets;
    }
}
