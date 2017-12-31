using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hachico.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(500)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(500)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [MaxLength(500)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [MaxLength(500)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [Range(1, 4, ErrorMessage = "Range 1-4") ]
        public string Type { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public int PhoneId { get; set; }
        [ForeignKey("PhoneId")]
        public virtual  Phone Phone { get; set; }
        //public List<PetPermission> PetPermissions { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
