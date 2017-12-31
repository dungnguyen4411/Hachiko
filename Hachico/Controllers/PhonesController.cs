using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hachico.Data;
using Hachico.Models;

namespace Hachico.Controllers
{
    [Produces("application/json")]
    [Route("api/Phones")]
    public class PhonesController : Controller
    {
        private readonly HachicoContext _context;

        public PhonesController(HachicoContext context)
        {
            _context = context;
        }

        // GET: api/Phones
        [HttpGet]
        public IEnumerable<Phone> GetPhones()
        {
            return _context.Phones;
        }

        // GET: api/Phones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phone = await _context.Phones.SingleOrDefaultAsync(m => m.Id == id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT: api/Phones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhone([FromRoute] int id, [FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.Id)
            {
                return BadRequest();
            }

            _context.Entry(phone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Phones
        [HttpPost]
        public async Task<IActionResult> PostPhone([FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhone", new { id = phone.Id }, phone);
        }

        // DELETE: api/Phones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phone = await _context.Phones.SingleOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();

            return Ok(phone);
        }

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }
    }
}