namespace Hachico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Pet
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SSID { get; set; }
        public DateTime DayOfBirth { get; set; }
        [Required]
        public int TypeAnimalId { get; set; }
        [Required]
        public Boolean Gender { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string NumberInformation { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("TypeAnimalId")]
        public virtual TypeAnimal Type { get; set; }
        [ForeignKey("SSID")]
        public virtual Device Device { get; set; }
        [Required]
        public virtual List<ImagePet> Images { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
