using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MinimalCandidateDto(Candidate candidate)
    {
        public string Id { get; } = candidate.Uid;
        [JsonPropertyName("display_name")] public string DisplayName { get; } = candidate.DisplayName;
    }
}
