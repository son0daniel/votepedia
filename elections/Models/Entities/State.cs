using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("state")]
    public class State : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("abbr")]
        [Required]
        public string Abbr { get; set; } = string.Empty;

        [Column("macroregion_id")]
        [Required]
        public long MacroregionId { get; set; }

        public Macroregion Macroregion { get; set; } = default!;

        public ICollection<Municipality> Municipalities { get; set; } = [];
    }
}
