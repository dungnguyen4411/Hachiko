using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double lng { get; set; }
        [Required]
        public double lat { get; set; }
        public string SSID { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [Range(1, 4)]
        public int Status { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
