using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }
    }
}
