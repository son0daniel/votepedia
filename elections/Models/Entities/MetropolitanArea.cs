using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("metropolitan_area")]
    public class MetropolitanArea : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("type")]
        [Required]
        public string Type { get; set; } = string.Empty;

        public ICollection<Municipality> Municipalities { get; set; } = [];
    }
}
