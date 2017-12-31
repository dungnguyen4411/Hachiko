using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hachico.Models
{
    public class PetStatus
    {
        [Key]
        public Guid PetId { get; set; }
        [Required]
        public int Type { get; set; }
        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        

    }
}
