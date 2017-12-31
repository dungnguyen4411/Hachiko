using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hachico.Data;
using Hachico.Models;
using Microsoft.Extensions.Primitives;
using Hachico.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Hachico.Controllers
{
    [Produces("application/json")]
    [Route("api/Devices")]
    public class DevicesController : BaseController
    {
        protected DevicesController(HachicoContext context)
        {
            _context = context;
        }

       

        //// GET: api/Devices/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDevice([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var device = await _context.Devices.SingleOrDefaultAsync(m => m.SSID == id);
        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(device);
        //}

        //// GET: api/Devices
        //[HttpGet]
        //public async Task<IActionResult> GetDevices()
        //{
        //    //if (!CheckAuthenDevice())
        //    //{
        //    //    return BadRequest("Not Authen");
        //    //}
        //    return Ok(Json(_context.Devices));
        //}
        //// PUT: api/Devices/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDevice([FromRoute] string id, [FromBody] Device device)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != device.SSID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(device).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DeviceExists(id))
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

        //// POST: api/Devices
        //[HttpPost]
        //public async Task<IActionResult> PostDevice([FromBody] Device device)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Devices.Add(device);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDevice", new { id = device.SSID }, device);
        //}

        //// DELETE: api/Devices/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDevice([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var device = await _context.Devices.SingleOrDefaultAsync(m => m.SSID == id);
        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Devices.Remove(device);
        //    await _context.SaveChangesAsync();

        //    return Ok(device);
        //}

        //private bool DeviceExists(string id)
        //{
        //    return _context.Devices.Any(e => e.SSID == id);
        //}
    }
}