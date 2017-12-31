using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hachico.Models
{
    public class PetPermission
    {
        public Guid PetId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Range(1, 3)]
        public int Type { get; set; }

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
