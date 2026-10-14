using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class CandidateElectionOverview(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, string role, Party party) : BaseElectionOverview(election, electionRound, ticketElectionRoundStatistic)
    {
        public string Role { get; } = role;
        public MinimalPartyDto Party { get; } = new MinimalPartyDto(party);
        public string Status { get; } = ticketElectionRoundStatistic.TicketStatus;
    }
}
