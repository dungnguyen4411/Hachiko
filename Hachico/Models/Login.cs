using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hachico.Models
{
    public class Login
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string FacebookId { get; set; }
        public string OneSignalID { get; set; }
        [Range(1,4)]
        [Required]
        public int Type { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
