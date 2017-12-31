using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.Models
{
    public class ImagePet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid PetId { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
    }
}
