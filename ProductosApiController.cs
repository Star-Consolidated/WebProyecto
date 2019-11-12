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
    [Route("api/Productos")]
    [ApiController]
    public class ProductosApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductosApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/ProductosApi
        /// <summary>
        /// Get all Productos
        /// </summary>
        /// <returns>All Productos</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
 
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/ProductosApi/5
        /// <summary>
        /// Get a specific Producto.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Productos</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/ProductosApi/5
        /// Update a specific Producto.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Productos
        ///     {
        ///         "campusID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="producto"></param>        
        /// <response code="400">the id is different to producto.ProductoID</response>   
        /// <response code="404">The Producto was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.ID)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/ProductosApi
        /// <summary>
        /// Creates a Producto.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Productos
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="producto"></param>
        /// <returns>A newly created Producto</returns>
        /// <response code="201">Returns the newly created Producto</response>
        /// <response code="400">If the Producto is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.ID }, producto);
        }

        // DELETE: api/ProductosApi/5
        /// <summary>
        /// Deletes a specific Producto.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ID == id);
        }
    }
}
