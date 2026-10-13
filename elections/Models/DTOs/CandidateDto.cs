using elections.Commons.Extensions;
using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class CandidateDto(Candidate candidate) : MinimalCandidateDto(candidate)
    {
        public string Name { get; set; } = candidate.Name;
        public string? Biography { get; set; } = candidate.Biography;
        [JsonPropertyName("is_deceased")] public bool IsDeceased { get; set; } = candidate.IsDeceased;
        [JsonPropertyName("birth_date")] public long? BirthDate { get; set; } = candidate.BirthDate?.ToUnixTimestamp();
        [JsonPropertyName("death_date")] public long? DeathDate { get; set; } = candidate.DeathDate?.ToUnixTimestamp();
    }
}
