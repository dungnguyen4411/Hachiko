using System;
using System.ComponentModel.DataAnnotations;

namespace Hachico.BindingModels
{
    public class LocationDurationBindingModel
    {
        [Required]
        public String SSID { get; set; }
        public DateTime FormDate { get; set; }
        public DateTime Todate { get; set; }
    }
    public class PostLocationBindingModel
    {
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
        public DateTime CreateAt { get; set; }
    }
}
