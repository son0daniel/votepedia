using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MinimalPartyDto(Party party)
    {
        public string Id { get; } = party.Uid;
        public string Abbr { get; } = party.Abbr;
        [JsonPropertyName("registration_year")] public int RegistrationYear { get; } = party.RegisteredAt.Year;
    }
}
