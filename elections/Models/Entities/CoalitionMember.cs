using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("coalition_member")]
    public class CoalitionMember : BaseEntity
    {
        [Column("coalition_id")]
        [Required]
        public long CoalitionId { get; set; }

        [Column("party_id")]
        public long? PartyId { get; set; }

        [Column("federation_id")]
        public long? FederationId { get; set; }

        public Coalition Coalition { get; set; } = default!;

        public Party? Party { get; set; }

        public Federation? Federation { get; set; }
    }
}
