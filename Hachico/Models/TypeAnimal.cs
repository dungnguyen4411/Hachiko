



namespace Hachico.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class TypeAnimal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Pet> Pets { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
