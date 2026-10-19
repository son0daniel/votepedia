using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("metropolitan_area_state")]
    public class MetropolitanAreaState : BaseEntity
    {
        [Column("metropolitan_area_id")]
        [Required]
        public long MetropolitanAreaId { get; set; }

        [Column("state_id")]
        [Required]
        public long StateId { get; set; }

        public MetropolitanArea MetropolitanArea { get; set; } = default!;

        public State State { get; set; } = default!;
    }
}
