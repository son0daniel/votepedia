using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class PartyElectionOverview(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, IEnumerable<TicketCandidateDto> candidates) : BaseElectionOverview(election, electionRound, ticketElectionRoundStatistic)
    {
        public List<TicketCandidateDto> Candidates { get; } = [.. candidates];
        public string Status { get; } = ticketElectionRoundStatistic.TicketStatus;
    }
}
