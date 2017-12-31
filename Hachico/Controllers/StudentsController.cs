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
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly HachicoContext _context;

        public StudentsController(HachicoContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet("GetAllStudent")]
        public IEnumerable<Student> GetAllStudent()
        {
            return _context.Students;
        }
        // GET: api/Students
        [HttpGet("Search/{name}")]
        public IEnumerable<Student> Search([FromRoute] string name)
        {
            return _context.Students.Where(m=>m.Name.IndexOf(name) != -1);
        }
        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost("EditStudent")]
        public async Task<IActionResult> EditStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.ID))
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

        // POST: api/Students
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}