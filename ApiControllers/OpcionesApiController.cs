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
    [Route("api/Opciones")]
    [ApiController]
    public class OpcionesApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public OpcionesApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/OpcionesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opcion>>> GetOpciones()
        {
            return await _context.Opciones.ToListAsync();
        }

        // GET: api/OpcionesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Opcion>> GetOpcion(int id)
        {
            var opcion = await _context.Opciones.FindAsync(id);

            if (opcion == null)
            {
                return NotFound();
            }

            return opcion;
        }

        // PUT: api/OpcionesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpcion(int id, Opcion opcion)
        {
            if (id != opcion.ID)
            {
                return BadRequest();
            }

            _context.Entry(opcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpcionExists(id))
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

        // POST: api/OpcionesApi
        [HttpPost]
        public async Task<ActionResult<Opcion>> PostOpcion(Opcion opcion)
        {
            _context.Opciones.Add(opcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpcion", new { id = opcion.ID }, opcion);
        }

        // DELETE: api/OpcionesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Opcion>> DeleteOpcion(int id)
        {
            var opcion = await _context.Opciones.FindAsync(id);
            if (opcion == null)
            {
                return NotFound();
            }

            _context.Opciones.Remove(opcion);
            await _context.SaveChangesAsync();

            return opcion;
        }

        private bool OpcionExists(int id)
        {
            return _context.Opciones.Any(e => e.ID == id);
        }
    }
}
