using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CelestialObject
        [HttpGet]
        public IEnumerable<CelestialObject> GetCelestialObjects()
        {
            return _context.CelestialObjects;
        }

        // GET: api/CelestialObject/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCelestialObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var celestialObject = await _context.CelestialObjects.FindAsync(id);

            if (celestialObject == null)
            {
                return NotFound();
            }

            return Ok(celestialObject);
        }

        // PUT: api/CelestialObject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCelestialObject([FromRoute] int id, [FromBody] CelestialObject celestialObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != celestialObject.Id)
            {
                return BadRequest();
            }

            _context.Entry(celestialObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CelestialObjectExists(id))
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

        // POST: api/CelestialObject
        [HttpPost]
        public async Task<IActionResult> PostCelestialObject([FromBody] CelestialObject celestialObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CelestialObjects.Add(celestialObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCelestialObject", new { id = celestialObject.Id }, celestialObject);
        }

        // DELETE: api/CelestialObject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCelestialObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var celestialObject = await _context.CelestialObjects.FindAsync(id);
            if (celestialObject == null)
            {
                return NotFound();
            }

            _context.CelestialObjects.Remove(celestialObject);
            await _context.SaveChangesAsync();

            return Ok(celestialObject);
        }

        private bool CelestialObjectExists(int id)
        {
            return _context.CelestialObjects.Any(e => e.Id == id);
        }
    }
}
