using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("federation")]
    public class Federation : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("display_name")]
        public string? DisplayName { get; set; }

        [Column("registered_at")]
        [Required]
        public DateOnly RegisteredAt { get; set; }

        [Column("dissolved_at")]
        public DateOnly? DissolvedAt { get; set; }

        public ICollection<FederationParty> FederationParties { get; set; } = [];

        public ICollection<CoalitionMember> CoalitionMembers { get; set; } = [];
    }
}
