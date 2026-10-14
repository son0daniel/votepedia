using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class ElectionOverview(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, Candidate candidate, Party party) : BaseElectionOverview(election, electionRound, ticketElectionRoundStatistic)
    {
        public MinimalCandidateDto Candidate { get; } = new MinimalCandidateDto(candidate);
        public MinimalPartyDto Party { get; } = new MinimalPartyDto(party);
    }
}
