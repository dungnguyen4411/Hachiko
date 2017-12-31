using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hachico.Auth
{
    public enum EnumTypeAuthenDevice
    {
        DeviceRequest = 1,
        AdminRequest = 2,
        UserRequest = 4,
    }
    public enum EnumTypeAuthenUser
    {
        User = 1,
        Supported = 2,
        Admin = 4,
    }
    public enum PetPermissionType
    {
          Boss = 1,
          Supporting = 2,
          Supported = 3
    }
    public enum PetStatusesType
    {
        Lost = 1,
        Normal = 2,
        Dead = 4,
        Sick = 5
    }

}
