using Hachico.Data;
using Microsoft.Extensions.Primitives;
using System.Linq;
namespace Hachico.Auth
{
    public class DeviceAuth
    {
        private HachicoContext _context;
        public DeviceAuth (HachicoContext context)
        {
            _context = context;
        }
        public bool IsAuthenticationDevice(StringValues token , EnumTypeAuthenDevice emunType)
        {
            switch (emunType) {
                case EnumTypeAuthenDevice.DeviceRequest:
                    var device = _context.Devices.FirstOrDefault(m => m.SSID.Equals(token));
                    return device != null;
            }
            return false;
        }
        public bool IsAuthenticatioUser(StringValues token, EnumTypeAuthenUser emunType)
        {
            switch (emunType)
            {
                case EnumTypeAuthenUser.User:
                    var device = _context.Logins.FirstOrDefault(m => m.AccessToken.Equals(token));
                    return device != null;
            }
            return false;
        }
    }
}
