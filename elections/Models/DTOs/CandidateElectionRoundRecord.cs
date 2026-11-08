using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class CandidateElectionRoundRecord(Party party, string role, int year, int roundNr, Coalition? coalition, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundRecord(year, roundNr, coalition, ticketElectionRoundStatistic)
    {
        public MinimalPartyDto Party { get; } = new MinimalPartyDto(party);
        public string Role { get; } = role;
    }
}
