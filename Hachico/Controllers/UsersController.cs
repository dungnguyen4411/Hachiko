using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hachico.Data;
using Hachico.Models;
using Hachico.BindingModels;
using Hachico.Auth;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Hachico.ViewModels;

namespace Hachico.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : BaseController
    {

        public UsersController(HachicoContext context)
        {
            _context = context;
        }
       
        #region for member
       
        [HttpGet("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation()
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            var user = await GetUserIdentify();
            string json = GetFacebookUserJSON(GetAuthorazationToken());
            FacebookUser oUser = JsonConvert.DeserializeObject<FacebookUser>(json);
            var pets = await _context.PetPermissions.Where(m => m.UserId.Equals(user.Id) && m.Type == (int)PetPermissionType.Boss).Select(m => m.Pet).ToListAsync();
            List<Models.User> users = new List<Models.User>();
            List<UserViewModels> userViewModel = new List<UserViewModels>();
            foreach (var element in pets)
            {
                var petsSupporting = await _context.PetPermissions.Where(m => m.PetId.Equals(element.Id) && m.Type == (int)PetPermissionType.Supporting).Include(m=>m.User).Select(m=>m.User).ToListAsync();
                users.AddRange(petsSupporting);
            }
            foreach (var ele in users)
            {
                var login = await _context.Logins.FirstOrDefaultAsync(m => m.UserId.Equals(ele.Id));
                userViewModel.Add(new UserViewModels { User = ele, FacebookID = login != null ? login.FacebookId : "0" });
            }
            return Ok(new { user = user , fid = oUser.id, listSupport = userViewModel });
        }
        [HttpGet("GetListUserRecuser")]
        public IActionResult GetListUserRecuser()
        {
            var listUsers = _context.Users.Where(m => int.Parse(m.Type) == (int)EnumTypeAuthenUser.Supported).ToList();
            var list = new List<Login>();
            foreach (var element in listUsers)
            {
                var Login = _context.Logins.SingleOrDefault(m => m.UserId.Equals(element.Id));
                list.Add(Login);
            }
            
            return Ok(new {ListLogin = list });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUserInformatio()
        {
            //var Phone = new Phone
            //{
            //    PhoneNumber = "0973658655",
            //    Type = 1,
            //    CreateDate = DateTime.Now,
            //    UpdateDate = DateTime.Now
            //};
            //_context.Phones.Add(Phone);
            //var user = new User
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = "Dung",
            //    LastName = "Nguyen",
            //    Email = "hoihoi441995@gmail.com",
            //    Address = "VN trái đất không phải ngoài hành tinh",
            //    Type = "1",
            //    Phone = Phone
            //};
            //_context.Users.Add(user);
            //var logined = new Login
            //{
            //    UserId = Guid.NewGuid(),
            //    AccessToken = "123456",
            //    FacebookId = "1",
            //    Provider = "Facebook",
            //    Type = 1,
            //    User = user
            //};
            //_context.Logins.Add(logined);
            //await _context.SaveChangesAsync();
            return Ok(_context.Users.ToList());
        }
        [HttpPost("UpdateInformationUser")]
        public async Task<IActionResult> UpdateInformationUser([FromBody] UserUpdateBindingModel model)
        {
            if (!CheckAuthenUser(EnumTypeAuthenUser.User))
            {
                return BadRequest("Not Authen");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var User = await GetUserIdentify();
                User.Address = String.IsNullOrEmpty(model.Address) ? User.Address : model.Address;
                User.FirstName = String.IsNullOrEmpty(model.FirstName) ? User.FirstName : model.FirstName;
                User.LastName = String.IsNullOrEmpty(model.LastName)? User.LastName : model.LastName;
                await _context.SaveChangesAsync();
                return Ok(User);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
         }
        
        #endregion
        #region
        //// POST: api/Users
        //[HttpPost]
        //public async Task<IActionResult> PostUser([FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}



        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
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
        #endregion
        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}