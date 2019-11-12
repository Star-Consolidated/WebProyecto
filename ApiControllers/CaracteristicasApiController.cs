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
    [Produces("application/json")]
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
        /// Get all Caracteristics 
        /// </summary>
        /// <returns>All Caracteristics </returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Caracteristica>>> GetCaracteristicas()
        {
            return await _context.Caracteristicas.ToListAsync();
        }
       
        // GET: api/CaracteristicasApi/5
        /// <summary>
        /// Get a specific Caracteristic.
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

        // GET: api/CaracteristicasApi/5/Productos
        /// <summary>
        /// Get a specific Caracteristic Productos.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Caracteristicas</response>
        /// <response code="200">OK</response>
        [HttpGet("{id}/Productos")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.Include(x=>x.Productos).FirstAsync(x=>x.ID.Equals(id));
  

            if (caracteristica == null)
            {
                return NotFound();
            }

            return caracteristica.Productos.ToList();
        }

        // PUT: api/CaracteristicasApi/5
        /// <summary>
        /// Update a specific Caracteristic.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Caracteristicas
        ///     {
        ///         "caracteristicaID": 0,
        ///         "nombre": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="caracteristica"></param>        
        /// <response code="400">the id is different to caracteristica.CaracteristicaID</response>   
        /// <response code="404">The Caracteristic not found</response>
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
        /// Creates a Caracteristic.
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
        /// <param name="caracteristica"></param>
        /// <returns>A newly created Caracteristic</returns>
        /// <response code="201">Returns the newly created Caracteristic</response>
        /// <response code="400">If the Caracteristic is null or invalid</response>            
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
        /// Deletes a specific Caracteristic.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]        public async Task<ActionResult<Caracteristica>> DeleteCaracteristica(int id)
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
