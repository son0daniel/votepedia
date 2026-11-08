using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class PartyElectionRoundRecord(List<TicketCandidateDto> ticketCandidates, int year, int roundNr, Coalition? coalition, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundRecord(year, roundNr, coalition, ticketElectionRoundStatistic)
    {
        public List<TicketCandidateDto> Candidates { get; } = ticketCandidates;
    }
}
