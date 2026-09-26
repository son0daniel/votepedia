using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("federation_party")]
    public class FederationParty : BaseEntity
    {
        [Column("federation_id")]
        [Required]
        public long FederationId { get; set; }

        [Column("party_id")]
        [Required]
        public long PartyId { get; set; }

        public Federation Federation { get; set; } = default!;

        public Party Party { get; set; } = default!;
    }
}
