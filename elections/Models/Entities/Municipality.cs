using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("municipality")]
    public class Municipality : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("tse_id")]
        [Required]
        public long TseId { get; set; }

        [Column("is_capital")]
        [Required]
        public bool IsCapital { get; set; }

        [Column("state_id")]
        [Required]
        public long StateId { get; set; }

        [Column("metropolitan_area_id")]
        public long? MetropolitanAreaId { get; set; }

        public State State { get; set; } = default!;

        public MetropolitanArea? MetropolitanArea { get; set; }
    }
}
