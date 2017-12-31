using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.BindingModels
{
    public class ExternalLoginBindingModel
    {
        // facebook or google 
        [Required]
        public string Provider { get; set; }
        public string Error { get; set; }
        public string OneSignalID { get; set; }
    }
    public class AdminloginBindingModel
    {
        [Required]
        
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
