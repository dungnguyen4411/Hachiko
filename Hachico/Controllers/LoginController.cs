using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hachico.BindingModels;
using System.Linq;
using Hachico.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using Hachico.Data;

namespace Hachico.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(HachicoContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("api/authentication/Login")]
        public async Task<IActionResult> GetExternalLogin([FromBody]  ExternalLoginBindingModel loginExternal)
        {
            if(loginExternal.Provider == "Facebook")
            {
                string json = GetFacebookUserJSON(GetAuthorazationToken());
                if(json != null)
                {
                    FacebookUser oUser = JsonConvert.DeserializeObject<FacebookUser>(json);
                    if (oUser != null)
                    {
                        var login = _context.Logins.FirstOrDefault(m => m.User.Email.Equals(oUser.email));
                        if (login == null)
                        {
                            _context.Logins.Add(new Login
                            {
                                FacebookId = oUser.id.ToString(),
                                AccessToken = GetAuthorazationToken(),
                                Provider = loginExternal.Provider,
                                Type = 1,
                                OneSignalID = loginExternal.OneSignalID,
                                User = new Models.User
                                {
                                    FirstName = oUser.first_name,
                                    LastName = oUser.last_name,
                                    Address = "",
                                    CreateDate = DateTime.Now,
                                    UpdateDate= DateTime.Now,
                                    Email = oUser.email,
                                    Type = "1",
                                    Phone = new Phone
                                    {
                                        PhoneNumber = "0312456825",
                                        CreateDate = DateTime.Now,
                                        UpdateDate = DateTime.Now,
                                        
                                    }
                                }
                            });
                        }
                        else
                        {
                            login.AccessToken = GetAuthorazationToken();
                            login.OneSignalID = loginExternal.OneSignalID;
                            _context.Entry(login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        _context.SaveChanges();
                    }
                    return Ok(GetAuthorazationToken());
                }
            }
            else
            {
                // call api google 
            }
            return BadRequest();    
        }
        private string GetEncodedHash(string password, string salt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string base64digest = Convert.ToBase64String(digest, 0, digest.Length);
            return base64digest.Substring(0, base64digest.Length - 2);
        }

        private const string username = "admin";
        private const string password = "Hachiko@4411";
        [HttpPost("api/authentication/AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminloginBindingModel model)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var salt = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            string base64digest = Convert.ToBase64String(salt, 0, salt.Length);

            if (!ModelState.IsValid || !model.Username.Equals(username) || !GetEncodedHash(model.Password, base64digest).Equals(GetEncodedHash(password, base64digest)) )
            {
                return BadRequest(ModelState);
            }
            return Ok("abc");
        }
    }
}