using Hachico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.ViewModels
{
    public class InformationPetViewModels
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SSID { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int TypeAnimalId { get; set; }
        public Boolean Gender { get; set; }
        public string Color { get; set; }
        public string NumberInformation { get; set; }
        public string Description { get; set; }
        public IQueryable<string> Images { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Battery { get; set; }
    }


    public class PetMissingViewModel
    {
        public Pet Pet { get; set; }
        public User User { get; set; }
        public Location Location { get; set; }
        public string OneSignalID { get; set; }
    }
}
