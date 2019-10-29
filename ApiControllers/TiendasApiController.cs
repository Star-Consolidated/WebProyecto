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
    [Route("api/Tiendas")]
    [ApiController]
    public class TiendasApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public TiendasApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/TiendasApi
        /// <summary>
        /// Get all Tiendas
        /// </summary>
        /// <returns>All Tiendas</returns>
        [HttpGet]
        [ProducesResponseType(200)]

        public async Task<ActionResult<IEnumerable<Tienda>>> GetTiendas()
        {
            return await _context.Tiendas.ToListAsync();
        }

        // GET: api/TiendasApi/5
        /// <summary>
        /// Get a specific Tienda.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Tiendas</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Tienda>> GetTienda(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);

            if (tienda == null)
            {
                return NotFound();
            }

            return tienda;
        }

        // PUT: api/TiendasApi/5
        /// <summary>
        /// Update a specific Tienda.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Tiendas
        ///     {
        ///         "tiendaID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="tienda"></param>        
        /// <response code="400">the id is different to tienda.TiendaID</response>   
        /// <response code="404">The tienda not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> PutTienda(int id, Tienda tienda)
        {
            if (id != tienda.ID)
            {
                return BadRequest();
            }

            _context.Entry(tienda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiendaExists(id))
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

        // POST: api/TiendasApi
        /// <summary>
        /// Creates a Tienda.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Tiendas
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="tienda"></param>
        /// <returns>A newly created Tienda</returns>
        /// <response code="201">Returns the newly created tienda</response>
        /// <response code="400">If the tienda is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Tienda>> PostTienda(Tienda tienda)
        {
            _context.Tiendas.Add(tienda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTienda", new { id = tienda.ID }, tienda);
        }

        // DELETE: api/TiendasApi/5
        /// <summary>
        /// Deletes a specific Tienda.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]


        public async Task<ActionResult<Tienda>> DeleteTienda(int id)
        {
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }

            _context.Tiendas.Remove(tienda);
            await _context.SaveChangesAsync();

            return tienda;
        }

        private bool TiendaExists(int id)
        {
            return _context.Tiendas.Any(e => e.ID == id);
        }
    }
}
