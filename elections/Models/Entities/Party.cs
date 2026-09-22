using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("party")]
    public class Party : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("abbr")]
        [Required]
        public string Abbr { get; set; } = string.Empty;

        [Column("nr_voteable")]
        [Required]
        public short NrVoteable { get; set; }

        [Column("is_active")]
        [Required]
        public bool IsActive { get; set; }

        [Column("founded_at")]
        public DateOnly? FoundedAt { get; set; }

        [Column("registered_at")]
        [Required]
        public DateOnly RegisteredAt { get; set; }

        [Column("dissolved_at")]
        public DateOnly? DissolvedAt { get; set; }

        public ICollection<CandidateParty> CandidateParties { get; set; } = [];
    }
}
