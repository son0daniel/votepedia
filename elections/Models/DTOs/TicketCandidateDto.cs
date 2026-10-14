using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class TicketCandidateDto(Candidate candidate, string role)
    {
        public MinimalCandidateDto Candidate { get; set; } = new MinimalCandidateDto(candidate);
        public string Role { get; set; } = role;
    }
}
