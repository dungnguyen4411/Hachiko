using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hachico.Data;
using Hachico.Models;
using Hachico.Auth;
using Hachico.BindingModels;

namespace Hachico.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationsController : BaseController
    {
        public LocationsController(HachicoContext context)
        {
            _context = context;
        }
        //[HttpPost("AddDevice")]
        //public void addDevice()
        //{
        //    _context.Devices.Add(new Device {
        //        SSID = "123456",
        //        CreateDate = new DateTime(),
        //        Status = 1
        //    });
        //    _context.SaveChanges();
        //}
        [HttpPost("TestReq")]
        public IActionResult TestReq(string abc)
        {
            var a = abc;
            return Ok(new {abcd = a});
        }
        [HttpPost("PostManyLocation")]
        public async Task<IActionResult> PostManyLocation([FromBody] PostLocationBindingModel[] listLocation)
        {
            if (!CheckAuthenDevice(EnumTypeAuthenDevice.DeviceRequest))
            {
                return BadRequest("Not Authen");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            for(int i = 0; i < listLocation.Length; i++)
            {
                var location = listLocation[i];
                var addLocation = new Location
                {
                    lat = location.lat,
                    lng = location.lng,
                    Status = location.Status,
                    CreateDate = location.CreateAt,
                    SSID = GetAuthenDevice(),
                };
                _context.Locations.Add(addLocation);
            }
            await _context.SaveChangesAsync();
            return Ok(_context.Locations.ToList());
        }
        [HttpPost("PostLocation")]
        public async Task<IActionResult> PostLocation([FromBody] PostLocationBindingModel location)
        {
            //if (!CheckAuthenDevice(EnumTypeAuthenDevice.DeviceRequest))
            //{
            //    return BadRequest("Not Authen");
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addLocation = new Location
            {
                lat = location.lat,
                lng = location.lng,
                Status = location.Status,
                CreateDate = location.CreateAt,
                SSID = _context.Devices.FirstOrDefault(m => m.SSID.Equals("123456")).SSID,
            };
            _context.Locations.Add(addLocation);
            await _context.SaveChangesAsync();
            return Ok(_context.Locations.ToList());
        }
        #region comment
        //// DELETE: api/Locations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Locations.Remove(location);
        //    await _context.SaveChangesAsync();

        //    return Ok(location);
        //}


        //// PUT: api/Locations/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLocation([FromRoute] int id, [FromBody] Location location)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != location.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(location).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LocationExists(id))
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


        //// GET: api/Locations/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetLocation([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);

        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(location);
        //}
#endregion
        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}