using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class CandidatePartyElectionRoundRecord(Candidate candidate, Party party, string role, int year, int roundNr, Coalition? coalition, TicketElectionRoundStatistic ticketElectionRoundStatistic) : CandidateElectionRoundRecord(party, role, year, roundNr, coalition, ticketElectionRoundStatistic)
    {
        public MinimalCandidateDto Candidate { get; } = new MinimalCandidateDto(candidate);
    }
}
