using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hachico.Data;
using Hachico.Auth;
using Hachico.BindingModels;
using Hachico.Models;
using Hachico.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace Hachico.Controllers
{
    [Produces("application/json")]
    [Route("api/Pets")]
    public class PetsController : BaseController
    {
        public PetsController(HachicoContext context)
        {
            _context = context;
        }
        // GET: api/Pets
        [HttpGet]
        [Route("GetListPet")]
        public async Task<IActionResult> GetListPet()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            var User = await GetUserIdentify();
            var pets = await  _context.PetPermissions.Where(m => m.UserId.Equals(User.Id) && m.Type == (int)PetPermissionType.Boss).Select(m=>m.Pet).ToListAsync();
            List<InformationPetViewModels> viewmodels = new List<InformationPetViewModels>();

            foreach (var pet in pets)
            {
                var images =  _context.ImagePets.Where(m => m.PetId.Equals(pet.Id)).Select(m=>m.ImageName);
                var device = await _context.DeviceDetails.FirstOrDefaultAsync(m => m.SSID.Equals(pet.SSID));
                viewmodels.Add(new InformationPetViewModels {
                    Id = pet.Id,
                    Color = pet.Color,
                    TypeAnimalId = pet.TypeAnimalId,
                    Name = pet.Name,
                    SSID = pet.SSID,
                    DayOfBirth = pet.DayOfBirth,
                    Gender = pet.Gender,
                    NumberInformation = pet.NumberInformation,
                    Description = pet.Description,
                    Images = images,
                    CreateDate = pet.CreateDate,
                    UpdateDate = pet.UpdateDate,
                    Battery = device != null ? device.Battery : 0
                });
            }
           
            return Ok(viewmodels);
        }
        [HttpGet("GetImagePet/{id}")]
        public async Task<IActionResult> GetImagePet([FromRoute] Guid id)
        {
            //var perList = new PetPermissionType[] { PetPermissionType.Boss, PetPermissionType.Supported, PetPermissionType.Supporting };
            //if (await IsPermission(id, perList) == false)
            //{
            //    return BadRequest("Not Permission");
            //}
            var a = await  _context.ImagePets.Where(m => m.PetId.Equals(id)).ToListAsync();
            return Ok(a);
        }
        // GET: api/Pets/5
        [HttpGet("GetPet/{id}")]
        public async Task<IActionResult> GetPet([FromRoute] Guid id)
        {
            var perList = new PetPermissionType[] { PetPermissionType.Boss, PetPermissionType.Supported, PetPermissionType.Supporting };
            if (await IsPermission(id, perList) == false)
            {
                return BadRequest("Not Permission");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pet = await _context.Pets.SingleOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }
        [HttpPost("AddBatteryDevice")]
        public async Task<IActionResult> AddBatteryDevice([FromBody] int numberOfBattery)
        {
            if (!CheckAuthenDevice(EnumTypeAuthenDevice.DeviceRequest))
            {
                return BadRequest("Not Authen");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var device = GetAuthenDevice();
            var deviceDetail = _context.DeviceDetails.Where(m => m.SSID.Equals(device)).FirstOrDefault();
            deviceDetail.Battery = numberOfBattery;
            await _context.SaveChangesAsync();
            return Ok(deviceDetail);
        }
        // GET: api/Pets/5
        [HttpGet("GetCurrentLocationPet/{id}")]
        public async Task<IActionResult> GetCurrentLocationPet([FromRoute]String SSID)
        {
            var perList = new PetPermissionType[] { PetPermissionType.Boss, PetPermissionType.Supported, PetPermissionType.Supporting };
            if (await IsPermissionSSID(SSID, perList) == false) 
            {
                return BadRequest("Not Permission");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_context.Locations.Where(m => m.SSID.Equals(SSID)).OrderByDescending(m => m.CreateDate).FirstOrDefault());
        }

        [HttpPost("GetLocationDurationPet")]
        public async Task<IActionResult> GetLocationDurationPet([FromBody] LocationDurationBindingModel model)
        {
            var perList = new PetPermissionType[] { PetPermissionType.Boss, PetPermissionType.Supported, PetPermissionType.Supporting };
            if (await IsPermissionSSID(model.SSID, perList) == false)
            {
                return BadRequest("Not Permission");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_context.Locations.Where(m => m.SSID.Equals(model.SSID) && m.CreateDate >= model.FormDate && m.CreateDate <= model.Todate ).OrderByDescending(m => m.CreateDate));
        }

        [HttpPost("UpdatePetDetailInformation")]
        public async Task<IActionResult> UpdatePetDetailInformation([FromBody] UpdatePetDetailBindingModel model)
        {
            var perList = new PetPermissionType[] { PetPermissionType.Boss};
            if (await IsPermissionSSID(model.SSID, perList) == false)
            {
                return BadRequest("Not Permission");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var pet = _context.Pets.FirstOrDefault(m => m.Id.Equals(model.Id));
                if (pet != null)
                {
                    pet.SSID = model.SSID != null ? model.SSID : pet.SSID;
                    pet.Name = model.Name != null ? model.Name : pet.Name;
                    pet.DayOfBirth = model.DayOfBirth != null ? model.DayOfBirth : pet.DayOfBirth;
                    pet.Gender = model.Gender;
                    pet.Color = model.Color != null ? model.Color : pet.Color;
                    pet.NumberInformation = model.NumberInformation != null ? model.NumberInformation : pet.NumberInformation;
                    pet.Description = model.Description != null ? model.Description : pet.Description;
                    pet.TypeAnimalId = model.TypeAnimalId;
                    pet.UpdateDate = model.UpdateDate;
                    await _context.SaveChangesAsync();
                    //return Ok(Json(new  { Message =  "Update succuessful" }));
                    return Ok(_context.Pets.ToList());
                }
                return BadRequest(Json(new { Message = "Update errors" }));
            }
            catch(Exception e)
            {
                return BadRequest(Json(new { Message = e }));
            }
            
        }
        [HttpGet("GetListTypeAnimal")]
        public IActionResult GetListTypeAnimal()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            return Ok(Json(_context.TypeAnimals.OrderByDescending(m=>m.CreateDate)));
        }
        [HttpGet("GetListPetSuported")]
        public IActionResult GetListPetSuported()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            var user = GetUserIdentify();
           var results =  _context.PetPermissions.Where(m => m.Type == (int)PetPermissionType.Supported && m.UserId.Equals(user.Id));
            return Ok(Json(new { results }));
        }
        [HttpGet("GetListPetSuporting")]
        public IActionResult GetListPetSuporting()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            var user = GetUserIdentify();
            var results = _context.PetPermissions.Where(m => m.Type == (int)PetPermissionType.Supporting && m.UserId.Equals(user.Id));
            return Ok(Json(new { results }));
        }

        [HttpGet("GetListPetMissing")]
        public IActionResult GetListPetMissing()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }

            var results = _context.PetStatuses.Where(m => m.Type == (int)PetStatusesType.Lost).Include(m=>m.Pet).ToList();
            List<PetMissingViewModel> pets = new List<PetMissingViewModel>();
            foreach (var ele in results)
            {
                var petPer = _context.PetPermissions.Include(m => m.User).FirstOrDefault(m=>m.PetId.Equals(ele.Pet.Id));
                var device = _context.Locations.Where(m => m.SSID.Equals(ele.Pet.SSID)).OrderByDescending(m=>m.CreateDate).ToList()[0];
                var Login = _context.Logins.FirstOrDefault(m=>m.UserId.Equals(petPer.UserId));
                pets.Add(new PetMissingViewModel {
                    Pet = ele.Pet,
                    User = petPer.User,
                    Location = device,
                    OneSignalID =  Login.OneSignalID,
                });
            }
            
            return Ok(Json(new { pets }));
        }
      
        [HttpGet("AddPetInDevice")]
        public IActionResult AddPetInDevice()
        {
            //if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            //{
            //    return BadRequest("Not Authen");
            //}
            var type = _context.TypeAnimals.SingleOrDefault(m => m.Id == 1);
            //var pet = new Pet
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Mẻ",
            //    SSID = "123456",
            //    Color = "Trắng xám đỏ",
            //    CreateDate = DateTime.Now,
            //    DayOfBirth = DateTime.Now,
            //    Device = _context.Devices.SingleOrDefault(m => m.SSID.Equals("123456")),
            //    Gender = true,
            //    NumberInformation = "12356",
            //    UpdateDate = DateTime.Now,
            //    Description = "abc",
            //    Type = type
            //};
            //_context.Pets.Add(pet);
            var PetPermission = new PetPermission
            {
                PetId = Guid.Parse("dd492054-fe62-4ac4-95f4-d2cdc5d1eea8"),
                UserId = Guid.Parse("20f16c7e-01f7-4cca-f808-08d54e11acd8"),
                Type = 2
            };
            _context.PetPermissions.Add(PetPermission);
            _context.SaveChanges();
            return Ok("Update Ok");
        }

        [HttpGet("ReportLostPet/{PetID}")]
        public async Task<IActionResult> ReportLostPetAsync([FromRoute]Guid PetID)
        {
            var perList = new PetPermissionType[] { PetPermissionType.Boss };
            //if (await IsPermissionPetID(PetID.ToString(), perList) == false)
            //{
            //    return BadRequest("Not Permission");
            //}
            try
            {
                var user = await GetUserIdentify();
                var petstatus = _context.PetStatuses.FirstOrDefault(m => m.PetId.Equals(PetID));
                if (petstatus != null)
                {
                    petstatus.Type = (int)PetStatusesType.Lost;
                }
                else
                {
                    _context.PetStatuses.Add(new Models.PetStatus
                    {
                        PetId = PetID,
                        Type = (int)PetStatusesType.Lost
                    });

                }
                _context.SaveChanges();
                // return Ok(Json(new { Message = "Reported Successfully"}));
                return Ok(_context.PetStatuses.Include(m=>m.Pet).ToList());
            }
            catch(Exception e)
            {
                return Ok(Json(new { Message = e }));
            }
        }
        [HttpPost("ReponseRequestFindPet")]
        public IActionResult ReponseRequestFindPet()
        {
            return Ok();
        }
        [HttpGet("ShowDatabaseDevices/{id}")]
        public IActionResult ShowDatabaseDevices([FromRoute] int id )
        {
            switch (id)
            {
                case 1:
                    var devices = _context.Devices.ToList();
                    return Ok(devices);
                case 2:
                    var login = _context.Logins.ToList();
                    return Ok(login);
                case 3:
                    var DetailDevices = _context.DeviceDetails.ToList();
                    return Ok(DetailDevices);
                case 4:
                    var ImagePet = _context.ImagePets.ToList();
                    return Ok(ImagePet);
                case 5:
                    var Location = _context.Locations.ToList();
                    return Ok(Location);
                case 6:
                    var Pet = _context.Pets.ToList();
                    return Ok(Pet);
                case 7:
                    var PetPer = _context.PetPermissions.ToList();
                    return Ok(PetPer);
                case 8:
                    var Petstt = _context.PetStatuses.ToList();
                    return Ok(Petstt);
                case 9:
                    var Phone = _context.Phones.ToList();
                    return Ok(Phone);
                case 10:
                    var TypeAnimal = _context.TypeAnimals.ToList();
                    return Ok(TypeAnimal);
                case 11:
                    var User = _context.Users;
                    return Ok(User);

            }
            return Ok();
        }
        #region comment
        //// GET: api/Pets
        //[HttpGet]
        //public IEnumerable<Pet> GetPets()
        //{
        //    return _context.Pets;
        //}


        //// PUT: api/Pets/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPet([FromRoute] Guid id, [FromBody] Pet pet)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != pet.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(pet).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PetExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Pets
        //[HttpPost]
        //public async Task<IActionResult> PostPet([FromBody] Pet pet)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Pets.Add(pet);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
        //}

        //// DELETE: api/Pets/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePet([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var pet = await _context.Pets.SingleOrDefaultAsync(m => m.Id == id);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Pets.Remove(pet);
        //    await _context.SaveChangesAsync();

        //    return Ok(pet);
        //}
        #endregion
        private bool PetExists(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}