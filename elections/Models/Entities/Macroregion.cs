using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("macroregion")]
    public class Macroregion : BaseEntityWithUid
    {
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("abbr")]
        [Required]
        public string Abbr { get; set; } = string.Empty;

        public ICollection<State> States { get; set; } = [];
    }
}
