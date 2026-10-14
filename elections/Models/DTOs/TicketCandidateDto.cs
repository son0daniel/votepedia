using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class TicketCandidateDto(Candidate candidate, string role)
    {
        public MinimalCandidateDto Candidate { get; } = new MinimalCandidateDto(candidate);
        public string Role { get; } = role;
    }
}
