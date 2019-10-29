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
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public UsuariosApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosApi
        /// <summary>
        /// Get all Usuarios
        /// </summary>
        /// <returns>All Usuarios</returns>
        [HttpGet]
        [ProducesResponseType(200)]


        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/UsuariosApi/5
        /// <summary>
        /// Get a specific Usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Usuarios</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/UsuariosApi/5
        /// Update a specific Usuario.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Usuarios
        ///     {
        ///         "usuarioID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="usuario"></param>        
        /// <response code="400">the id is different to usuario.UsuarioID</response>   
        /// <response code="404">The usuario not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/UsuariosApi
        /// Creates a Usuario.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Usuarios
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="usuario"></param>
        /// <returns>A newly created Usuario</returns>
        /// <response code="201">Returns the newly created usuario</response>
        /// <response code="400">If the usuario is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.ID }, usuario);
        }

        // DELETE: api/UsuariosApi/5
        /// <summary>
        /// Deletes a specific Usuario.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]

        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}
