using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class TicketCandidatePartyDto(Candidate candidate, Party party, string role, int levelNr) : TicketCandidateDto(candidate, role, levelNr)
    {
        public MinimalPartyDto Party { get; } = new MinimalPartyDto(party);
    }
}
