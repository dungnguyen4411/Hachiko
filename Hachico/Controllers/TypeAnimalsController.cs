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
    [Route("api/TypeAnimals")]
    public class TypeAnimalsController : Controller
    {
        private readonly HachicoContext _context;

        public TypeAnimalsController(HachicoContext context)
        {
            _context = context;
        }

        // GET: api/TypeAnimals
        [HttpGet]
        public IEnumerable<TypeAnimal> GetTypeAnimals()
        {
            return _context.TypeAnimals;
        }

        // GET: api/TypeAnimals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeAnimal = await _context.TypeAnimals.SingleOrDefaultAsync(m => m.Id == id);

            if (typeAnimal == null)
            {
                return NotFound();
            }

            return Ok(typeAnimal);
        }

        // PUT: api/TypeAnimals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeAnimal([FromRoute] int id, [FromBody] TypeAnimal typeAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeAnimal.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeAnimal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeAnimalExists(id))
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

        // POST: api/TypeAnimals
        [HttpPost]
        public async Task<IActionResult> PostTypeAnimal([FromBody] TypeAnimal typeAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TypeAnimals.Add(typeAnimal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeAnimal", new { id = typeAnimal.Id }, typeAnimal);
        }

        // DELETE: api/TypeAnimals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeAnimal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeAnimal = await _context.TypeAnimals.SingleOrDefaultAsync(m => m.Id == id);
            if (typeAnimal == null)
            {
                return NotFound();
            }

            _context.TypeAnimals.Remove(typeAnimal);
            await _context.SaveChangesAsync();

            return Ok(typeAnimal);
        }

        private bool TypeAnimalExists(int id)
        {
            return _context.TypeAnimals.Any(e => e.Id == id);
        }
    }
}