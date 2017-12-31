using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.BindingModels
{
    public class UserUpdateBindingModel
    {
        [MaxLength(500)]
        public string FirstName { get; set; }
        [MaxLength(500)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [MaxLength(500)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }

    }
}
