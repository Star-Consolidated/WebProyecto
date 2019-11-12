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
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public CategoriasApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/CategoriasApi
        /// <summary>
        /// Get all Categorias
        /// </summary>
        /// <returns>All Categorias</returns>
        /// <response code="200">OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/CategoriasApi/5
        /// <summary>
        /// Get a specific Categoria.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">The id not found in Categorias</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }
        
        
        // PUT: api/CategoriasApi/5
        /// <summary>
        /// Update a specific Categoria.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Categorias
        ///     {
        ///         "categoriaID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="categoria"></param>        
        /// <response code="400">the id is different to categoria.CategoriaID</response>   
        /// <response code="404">The Categoria was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/CategoriasApi
        /// <summary>
        /// Creates a Categoria.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Categorias
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="categoria"></param>
        /// <returns>A newly created Categoria</returns>
        /// <response code="201">Returns the newly created Categoria</response>
        /// <response code="400">If the Categoria is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.ID }, categoria);
        }

        // DELETE: api/CategoriasApi/5
        /// <summary>
        /// Deletes a specific Categoria.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.ID == id);
        }
    }
}
