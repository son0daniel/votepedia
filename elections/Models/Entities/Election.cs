using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("election")]
    public class Election : BaseEntityWithUid
    {
        [Column("year")]
        [Required]
        public int Year { get; set; }

        [Column("role")]
        [Required]
        public string Role { get; set; } = string.Empty;

        [Column("type")]
        [Required]
        public string Type { get; set; } = string.Empty;

        [Column("municipality_id")]
        public long? MunicipalityId { get; set; }

        [Column("state_id")]
        public long? StateId { get; set; }

        public Municipality? Municipality { get; set; }

        public State? State { get; set; }

        public ICollection<ElectionRound> ElectionRounds { get; set; } = [];
    }
}
