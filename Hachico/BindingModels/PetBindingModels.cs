using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.BindingModels
{
    public class UpdatePetDetailBindingModel
    {
        [Required]
        public Guid Id { get; set; }
        [MinLength(6)]
        public string Name { get; set; }
        [StringLength(10)]
        public string SSID { get; set; }
        public DateTime DayOfBirth { get; set; }
        public Boolean Gender { get; set; }
        [MinLength(3)]
        public string Color { get; set; }
        [MinLength(6)]
        public string NumberInformation { get; set; }
        [MinLength(6)]
        public string Description { get; set; }
        [Required]
        public int TypeAnimalId { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
