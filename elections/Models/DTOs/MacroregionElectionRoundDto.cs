using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class MacroregionElectionRoundDto(List<MinimalTicketDto> tickets, Macroregion macroregion, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic)
    {
        public string Macroregion { get; } = macroregion.Name;
        public string Abbr { get; } = macroregion.Abbr;
        public List<MinimalTicketDto> Tickets { get; } = tickets;
    }
}
