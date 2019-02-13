using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTraq.Api.Models;

namespace BugTraq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly BugTraqContext _context;

        public BugsController(BugTraqContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugs()
        {
            return await _context.Bugs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);

            if (bug == null)
            {
                return NotFound();
            }

            return bug;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug(int id, [FromForm]Bug bug)
        {
            if (id != bug.BugId)
            {
                return BadRequest();
            }

            _context.Entry(bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Bug>> PostBug([FromForm]Bug bug)
        {
            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBug", new { id = bug.BugId }, bug);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Bug>> DeleteBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            return bug;
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
