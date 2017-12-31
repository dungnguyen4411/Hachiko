using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.Models
{
    public class Student
    {
        [Key]
        public String ID { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
