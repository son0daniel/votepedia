using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("candidate_party")]
    public class CandidateParty : BaseEntity
    {
        [Column("candidate_id")]
        [Required]
        public long CandidateId { get; set; }

        [Column("party_id")]
        [Required]
        public long PartyId { get; set; }

        public Candidate Candidate { get; set; } = default!;

        public Party Party { get; set; } = default!;
    }
}
