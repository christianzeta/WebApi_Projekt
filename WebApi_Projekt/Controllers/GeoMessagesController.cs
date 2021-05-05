using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Projekt.Data;
using WebApi_Projekt.Models;

namespace WebApi_Projekt.Controllers
{
    [Route("api/v1/geo-comments")]
    [ApiController]
    public class GeoMessagesController : ControllerBase
    {
        private readonly Context _context;

        public GeoMessagesController(Context context)
        {
            _context = context;
        }

        // GET: api/v1/geo-comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoMessage>>> GetGeoMessages()
        {
            return await _context.GeoMessages.ToListAsync();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/v1/geo-comments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoMessage>> GetGeoMessage(int id)
        {
            var geoMessage = await _context.GeoMessages.FindAsync(id);

            if (geoMessage == null)
            {
                return NotFound();
            }

            return Ok(geoMessage);
        }


        // POST: api/v1/geo-comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GeoMessage>> PostGeoMessage([FromBody]GeoMessage geoMessage)
        {
            _context.GeoMessages.Add(geoMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeoMessage", new { id = geoMessage.Id }, geoMessage);
        }
    }
}
