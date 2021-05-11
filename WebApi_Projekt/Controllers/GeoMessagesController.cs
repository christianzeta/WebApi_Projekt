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
        public async Task<ActionResult<IEnumerable<GeoMessageV1Get>>> GetGeoMessages()
        {
            var geoMessage = await _context.GeoMessages.ToListAsync();

            List<GeoMessageV1Get> geoMessageV1List = new List<GeoMessageV1Get>();
            foreach (var message in geoMessage)
            {
                GeoMessageV1Get geoMessageV1Get = new GeoMessageV1Get
                {
                    Id = message.Id,
                    Message = message.Body,
                    Latitude = message.Latitude,
                    Longitude = message.Longitude
                };
                geoMessageV1List.Add(geoMessageV1Get);
            }

            return geoMessageV1List;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/v1/geo-comments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<GeoMessageV1Get>> GetGeoMessage(int id)
        {
            var geoMessage = await _context.GeoMessages.FindAsync(id);

            if (geoMessage == null)
            {
                return NotFound();
            }

            GeoMessageV1 geoMessageV1Get = new GeoMessageV1Get
            {
                Id = geoMessage.Id,
                Message = geoMessage.Body,
                Latitude = geoMessage.Latitude,
                Longitude = geoMessage.Longitude
            };

            return Ok(geoMessageV1Get);
        }


        // POST: api/v1/geo-comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GeoMessageV1>> PostGeoMessage([FromBody]GeoMessageV1 geoMessageV1)
        {
            GeoMessage geoMessage = new GeoMessage
            {
                Body = geoMessageV1.Message,
                Author = null,
                Title = null,
                Longitude = geoMessageV1.Longitude,
                Latitude = geoMessageV1.Latitude,
            };

            _context.GeoMessages.Add(geoMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeoMessage", new { id = geoMessage.Id }, geoMessage);
        }

        public class GeoMessageV1
        {
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class GeoMessageV1Get : GeoMessageV1
        {
            public int Id { get; set; }
        }

        


    }
}
