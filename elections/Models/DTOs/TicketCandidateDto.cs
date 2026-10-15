using elections.Commons.Utils;
using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class TicketCandidateDto(Candidate candidate, string role, int levelNr)
    {
        public MinimalCandidateDto Candidate { get; } = new MinimalCandidateDto(candidate);
        [JsonIgnore] public int LevelNr { get; } = levelNr;
        public string Role => RoleResolver.GetOfficeRole(role, LevelNr);
    }
}
