using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elections.Models.Entities
{
    [Table("election_round")]
    public class ElectionRound : BaseEntityWithUid
    {
        [Column("nr_round")]
        [Required]
        public int NrRound { get; set; }

        [Column("held_at")]
        [Required]
        public DateOnly HeldAt { get; set; }

        [Column("election_id")]
        [Required]
        public long ElectionId { get; set; }

        public Election Election { get; set; } = default!;
    }
}
