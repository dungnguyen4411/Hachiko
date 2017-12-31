using Hachico.Auth;
using Hachico.Data;
using Hachico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hachico.Controllers
{
    public class BaseController : Controller
    {
        protected  HachicoContext _context;
       
        protected bool CheckAuthenDevice(EnumTypeAuthenDevice emunType)
        {
            StringValues token;
            var result = Request.Headers.TryGetValue("Authorization", out token);
            if (result)
            {
                DeviceAuth deviceAuth = new DeviceAuth(_context);
                return deviceAuth.IsAuthenticationDevice(token, emunType);
            }
            return false;
        }

        protected string GetAuthenDevice()
        {
            StringValues token;
            var result = Request.Headers.TryGetValue("Authorization", out token);
            if (result)
            {
                DeviceAuth deviceAuth = new DeviceAuth(_context);
                return token;
            }
            return null;
        }
        protected StringValues GetAuthorazationToken()
        {
            StringValues token;
            var result = Request.Headers.TryGetValue("Authorization", out token);
            return result ? token : token;
        }
        protected async Task<User> GetUserIdentify()
        {
            StringValues token;
            var result = Request.Headers.TryGetValue("Authorization", out token);
            if (result)
            {
                 var loginid = await _context.Logins.SingleOrDefaultAsync(m => m.AccessToken.Equals(token));
                return await _context.Users.Include(mt=>mt.Phone).SingleOrDefaultAsync(m=>m.Id.Equals(loginid.UserId));
            }
            return null;
        }
        protected async Task<bool> IsPermissionSSID(string SSID, PetPermissionType[] types)
        {
            var user = GetUserIdentify();
            var pet = await _context.Pets.FirstOrDefaultAsync(m => m.SSID.Equals(SSID));
            List<Pet> pets = new List<Pet>();
            if (pet != null)
            {
                for(int i = 0; i < types.Length; i++)
                {
                   var obj = _context.PetPermissions
                            .FirstOrDefault(m => m.PetId.Equals(pet.Id)
                            && m.Type == (int)types[i] && m.UserId.Equals(user.Id));
                    if(obj != null)
                    {
                        pets.Add(obj.Pet);
                    }
                }
            }
            return pets.Count != 0 && GetUserIdentify() != null;
        }

        protected async Task<bool> IsPermissionPetID(string PetID, PetPermissionType[] types)
        {
            var user = await GetUserIdentify();
            List<Pet> pets = new List<Pet>();
           
                for (int i = 0; i < types.Length; i++)
                {
                    var obj = await _context.PetPermissions
                             .FirstOrDefaultAsync(m => m.PetId.Equals(PetID)
                             &&  m.UserId.Equals(user.Id));
                    if (obj != null)
                    {
                        pets.Add(obj.Pet);
                    }
                }
            return pets.Count != 0 && GetUserIdentify() != null;
        }
        protected bool CheckAuthenUser(EnumTypeAuthenUser emunType)
        {
            StringValues token;
            var result = Request.Headers.TryGetValue("Authorization", out token);
            if (result)
            {
                DeviceAuth deviceAuth = new DeviceAuth(_context);
                return deviceAuth.IsAuthenticatioUser(token, emunType);
            }
            return false;
        }
        protected async Task<bool> IsPermission(Guid PetId, PetPermissionType[] types)
        {
            var user = await  GetUserIdentify();
            var petpermission = _context.PetPermissions.FirstOrDefault(m=>m.PetId.Equals(PetId) && m.UserId.Equals(user.Id) );
            var pet = _context.Pets.FirstOrDefault(m => m.Id.Equals(PetId));
            List<Pet> pets = new List<Pet>();
            if (pet != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    var obj = _context.PetPermissions
                             .FirstOrDefault(m => m.PetId.Equals(pet.Id)
                             && m.Type == (int)types[i] && m.UserId.Equals(user.Id));
                    if (obj != null)
                    {
                        pets.Add(obj.Pet);
                    }
                }
            }
            return pets.Count != 0 && GetUserIdentify() != null;
        }
        protected string GetFacebookUserJSON(string access_token)
        {
            try
            {
                string url = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name,first_name,last_name,link", access_token);
                WebClient wc = new WebClient();
                Stream data = wc.OpenRead(url);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s;
            }
            catch
            {
                return null;
            }
        }
    }
}
