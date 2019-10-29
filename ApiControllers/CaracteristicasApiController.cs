using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProyecto.Data;
using WebProyecto.Models;

namespace WebProyecto.ApiControllers
{
    
    [Route("api/Caracteristicas")]
    [ApiController]
    public class CaracteristicasApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public CaracteristicasApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/CaracteristicasApi
        /// <summary>
        /// Get all Caracteristicas
        /// </summary>
        /// <returns>All Caracteristicas</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Caracteristica>>> GetCaracteristicas()
        {
            return await _context.Caracteristicas.ToListAsync();
        }

        // GET: api/CaracteristicasApi/5
        /// <summary>
        /// Get a specific Caracteristicas.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Caracteristicas</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Caracteristica>> GetCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);

            if (caracteristica == null)
            {
                return NotFound();
            }

            return caracteristica;
        }

        // PUT: api/CaracteristicasApi/5
        /// <summary>
        /// Update a specific Caracteritica.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Caracteristica
        ///     {
        ///         "CaracteristicaID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="campus"></param>        
        /// <response code="400">the id is different to caracteristica.CaracteristicaID</response>   
        /// <response code="404">The Caracteristica not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCaracteristica(int id, Caracteristica caracteristica)
        {
            if (id != caracteristica.ID)
            {
                return BadRequest();
            }

            _context.Entry(caracteristica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristicaExists(id))
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

        // POST: api/CaracteristicasApi
        /// <summary>
        /// Creates a Caracteristica.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Caracteristicas
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="campus"></param>
        /// <returns>A newly created Caracteristica</returns>
        /// <response code="201">Returns the newly created Caracteristica</response>
        /// <response code="400">If the campus is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Caracteristica>> PostCaracteristica(Caracteristica caracteristica)
        {
            _context.Caracteristicas.Add(caracteristica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristica", new { id = caracteristica.ID }, caracteristica);
        }

        // DELETE: api/CaracteristicasApi/5
        /// <summary>
        /// Deletes a specific Caracteristica.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Caracteristica>> DeleteCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            _context.Caracteristicas.Remove(caracteristica);
            await _context.SaveChangesAsync();

            return caracteristica;
        }

        private bool CaracteristicaExists(int id)
        {
            return _context.Caracteristicas.Any(e => e.ID == id);
        }
    }
}
