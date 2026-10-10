using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MinimalCandidateDto
    {
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("display_name")] public string DisplayName { get; set; } = string.Empty;

        public static MinimalCandidateDto New(Candidate candidate)
        {
            return new MinimalCandidateDto
            {
                Id = candidate.Uid,
                DisplayName = candidate.DisplayName
            };
        }
    }
}
