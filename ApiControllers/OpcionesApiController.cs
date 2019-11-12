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
        /// <summary>
        /// Get all Opciones
        /// </summary>
        /// <returns>All Opciones</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Opcion>>> GetOpciones()
        {
            return await _context.Opciones.ToListAsync();
        }

        // GET: api/OpcionesApi/5
        /// <summary>
        /// Get a specific Opcion.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Opciones</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
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
         /// <summary>
        /// Update a specific Opcion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Opciones
        ///     {
        ///         "campusID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="opcion"></param>        
        /// <response code="400">the id is different to opcion.OpcionID</response>   
        /// <response code="404">The Opcion was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Creates a Opcion.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Opciones
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="opcion"></param>
        /// <returns>A newly created Opcion</returns>
        /// <response code="201">Returns the newly created Opcion</response>
        /// <response code="400">If the Opcion is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Opcion>> PostOpcion(Opcion opcion)
        {
            _context.Opciones.Add(opcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpcion", new { id = opcion.ID }, opcion);
        }

        // DELETE: api/OpcionesApi/5
        /// <summary>
        /// Deletes a specific Opcion.
        /// </summary>
        /// <param name="id"></param>   
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
