using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.Models
{
    public class DeviceDetail
    {
        [Key]
        public String SSID { get; set; }
        [Required]
        [Range(1, 4)]
        public int TypeException { get; set; }
        [Required]
        public bool Status { get; set; }
        [Range(0, 100)]
        public int Battery { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
