namespace Hachico.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, 4)]
        public int Type { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
