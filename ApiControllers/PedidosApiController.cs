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
    [Route("api/Pedidos")]
    [ApiController]
    public class PedidosApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public PedidosApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/PedidosApi
         /// <summary>
        /// Get all Pedidos
        /// </summary>
        /// <returns>All Pedidos</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/PedidosApi/5
        /// <summary>
        /// Get a specific Pedidos.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Pedidos</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/PedidosApi/5
        /// <summary>
        /// Update a specific Pedido.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Pedidos
        ///     {
        ///         "campusID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="pedido"></param>        
        /// <response code="400">the id is different to pedido.PedidoID</response>   
        /// <response code="404">The Pedido was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/PedidosApi
        /// <summary>
        /// Creates a Pedido.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Pedidos
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="pedido"></param>
        /// <returns>A newly created Pedido</returns>
        /// <response code="201">Returns the newly created Pedido</response>
        /// <response code="400">If the Pedido is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.ID }, pedido);
        }

        // DELETE: api/PedidosApi/5
        /// <summary>
        /// Deletes a specific Pedido.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.ID == id);
        }
    }
}
