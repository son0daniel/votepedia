using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MinimalPartyDto(Party party)
    {
        public string Id { get; set; } = party.Uid;
        public string Abbr { get; set; } = party.Abbr;
        [JsonPropertyName("registration_year")] public int RegistrationYear { get; set; } = party.RegisteredAt.Year;
    }
}
