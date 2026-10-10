using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MinimalPartyDto
    {
        public string Id { get; set; } = string.Empty;
        public string Abbr { get; set; } = string.Empty;
        [JsonPropertyName("registration_year")] public int RegistrationYear { get; set; }

        public static MinimalPartyDto New(Party party)
        {
            return new MinimalPartyDto
            {
                Id = party.Uid,
                Abbr = party.Abbr,
                RegistrationYear = party.RegisteredAt.Year
            };
        }
    }
}
