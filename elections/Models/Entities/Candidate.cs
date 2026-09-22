using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("candidate")]
    public class Candidate : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("display_name")]
        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Column("tse_name")]
        [Required]
        public string TseName { get; set; } = string.Empty;

        [Column("is_deceased")]
        [Required]
        public bool IsDeceased { get; set; }

        [Column("birth_date")]
        public DateOnly? BirthDate { get; set; }

        [Column("death_date")]
        public DateOnly? DeathDate { get; set; }

        [Column("biography")]
        public string? Biography { get; set; }

        public ICollection<CandidateParty> CandidateParties { get; set; } = [];
    }
}
