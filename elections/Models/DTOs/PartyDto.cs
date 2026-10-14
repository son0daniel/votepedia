using elections.Commons.Extensions;
using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class PartyDto(Party party) : MinimalPartyDto(party)
    {
        public string Name { get; set; } = party.Name;
        [JsonPropertyName("voteable_nr")] public int VoteableNr { get; set; } = party.NrVoteable;
        [JsonPropertyName("is_active")] public bool IsActive { get; set; } = party.IsActive;
        public long? Foundation { get; set; } = party.FoundedAt?.ToUnixTimestamp();
        public long Registration { get; set; } = party.RegisteredAt.ToUnixTimestamp();
        public long? Dissolution { get; set; } = party.DissolvedAt?.ToUnixTimestamp();
    }
}
