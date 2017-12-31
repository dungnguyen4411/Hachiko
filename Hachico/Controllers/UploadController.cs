using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Hachico.Auth;
using Microsoft.EntityFrameworkCore;
using Hachico.Data;

namespace Hachico.Controllers
{
    [Route("api/Upload")]
    [Produces("application/json")]
    [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
    public class UploadController : BaseController
    {
        public UploadController(HachicoContext context)
        {
            _context = context;
        }
      
        [HttpPost("PostProfilePicture/{id}")]
        public async Task<IActionResult> PostProfilePicture(IFormFile file, [FromRoute] Guid id)
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
            if(pet == null)
            {
                return BadRequest();
            }
           
            var stream = file.OpenReadStream();
            var name = file.FileName;
            byte[] a ;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                a = memoryStream.ToArray();
            }
            string base64String = Convert.ToBase64String(a);
            _context.ImagePets.Add(new Models.ImagePet
            {
                PetId = pet.Id,
                CreateDate = DateTime.Now,
                ImageName = base64String,
            });
            _context.SaveChanges();
            return Ok(base64String); //null just to make error 
        }
    }
}