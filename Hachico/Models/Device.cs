

namespace Hachico.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Device
    {
        [Key]
        public String SSID { get; set; }
        [Required]
        [Range(1, 4)]
        public int Status { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [ForeignKey("SSID")]
        public virtual DeviceDetail DeviceDetail { get; set; }
    }
}
