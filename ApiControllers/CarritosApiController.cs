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
    [Route("api/Carritos")]
    [ApiController]
    public class CarritosApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public CarritosApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/CarritosApi
         /// <summary>
        /// Get all Carritos
        /// </summary>
        /// <returns>All Carritos</returns>
        /// <Response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            return await _context.Carritos.ToListAsync();
        }

        // GET: api/CarritosApi/5
         /// <summary>
        /// Get a specific Carrito.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Carritos</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Carrito>> GetCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        // PUT: api/CarritosApi/5
        /// <summary>
        /// Update a specific Carrito.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Carritos
        ///     {
        ///         "carritosID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="campus"></param>        
        /// <response code="400">the id is different to carrito.CarritooID</response>   
        /// <response code="404">The Carrito was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.ID)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
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

        // POST: api/CarritosApi
        /// <summary>
        /// Creates a Carrito.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Carritos
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="Carrito"></param>
        /// <returns>A newly created Carrito</returns>
        /// <response code="201">Returns the newly created Carrito</response>
        /// <response code="400">If the Carrito is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito", new { id = carrito.ID }, carrito);
        }

        // DELETE: api/CarritosApi/5
        /// <summary>
        /// Deletes a specific Carrito.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carrito>> DeleteCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return carrito;
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.ID == id);
        }
    }
}
