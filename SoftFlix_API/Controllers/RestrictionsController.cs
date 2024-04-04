﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftFlix_API.Data;
using SoftFlix_API.Models;

namespace SoftFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RestrictionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Restrictions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restriction>>> GetRestrictions()
        {
          if (_context.Restrictions == null)
          {
              return NotFound();
          }
            return await _context.Restrictions.ToListAsync();
        }

        // GET: api/Restrictions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restriction>> GetRestriction(byte id)
        {
          if (_context.Restrictions == null)
          {
              return NotFound();
          }
            var restriction = await _context.Restrictions.FindAsync(id);

            if (restriction == null)
            {
                return NotFound();
            }

            return restriction;
        }

        // PUT: api/Restrictions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestriction(byte id, Restriction restriction)
        {
            if (id != restriction.Id)
            {
                return BadRequest();
            }

            _context.Entry(restriction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestrictionExists(id))
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

        // POST: api/Restrictions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restriction>> PostRestriction(Restriction restriction)
        {
          if (_context.Restrictions == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Restrictions'  is null.");
          }
            _context.Restrictions.Add(restriction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestrictionExists(restriction.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestriction", new { id = restriction.Id }, restriction);
        }

        // DELETE: api/Restrictions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestriction(byte id)
        {
            if (_context.Restrictions == null)
            {
                return NotFound();
            }
            var restriction = await _context.Restrictions.FindAsync(id);
            if (restriction == null)
            {
                return NotFound();
            }

            _context.Restrictions.Remove(restriction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestrictionExists(byte id)
        {
            return (_context.Restrictions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
