using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    public class BaseEntityWithUid : BaseEntity
    {
        [Column("uid")]
        [Required]
        public string Uid { get; set; } = string.Empty;
    }
}
