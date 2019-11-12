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
    [Route("api/Detalle_Pedidos")]
    [ApiController]
    public class Detalle_PedidosApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public Detalle_PedidosApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/Detalle_PedidosApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detalle_Pedido>>> GetDetalle_Pedidos()
        {
            return await _context.Detalle_Pedidos.ToListAsync();
        }

        // GET: api/Detalle_PedidosApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detalle_Pedido>> GetDetalle_Pedido(int id)
        {
            var detalle_Pedido = await _context.Detalle_Pedidos.FindAsync(id);

            if (detalle_Pedido == null)
            {
                return NotFound();
            }

            return detalle_Pedido;
        }

        // PUT: api/Detalle_PedidosApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle_Pedido(int id, Detalle_Pedido detalle_Pedido)
        {
            if (id != detalle_Pedido.ID)
            {
                return BadRequest();
            }

            _context.Entry(detalle_Pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Detalle_PedidoExists(id))
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

        // POST: api/Detalle_PedidosApi
        [HttpPost]
        public async Task<ActionResult<Detalle_Pedido>> PostDetalle_Pedido(Detalle_Pedido detalle_Pedido)
        {
            _context.Detalle_Pedidos.Add(detalle_Pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalle_Pedido", new { id = detalle_Pedido.ID }, detalle_Pedido);
        }

        // DELETE: api/Detalle_PedidosApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Detalle_Pedido>> DeleteDetalle_Pedido(int id)
        {
            var detalle_Pedido = await _context.Detalle_Pedidos.FindAsync(id);
            if (detalle_Pedido == null)
            {
                return NotFound();
            }

            _context.Detalle_Pedidos.Remove(detalle_Pedido);
            await _context.SaveChangesAsync();

            return detalle_Pedido;
        }

        private bool Detalle_PedidoExists(int id)
        {
            return _context.Detalle_Pedidos.Any(e => e.ID == id);
        }
    }
}
