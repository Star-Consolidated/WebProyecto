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
    [Route("api/Producto_Categorias")]
    [ApiController]
    public class Producto_CategoriasApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public Producto_CategoriasApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/Producto_CategoriasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto_Categoria>>> GetProducto_Categorias()
        {
            return await _context.Producto_Categorias.ToListAsync();
        }

        // GET: api/Producto_CategoriasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto_Categoria>> GetProducto_Categoria(int id)
        {
            var producto_Categoria = await _context.Producto_Categorias.FindAsync(id);

            if (producto_Categoria == null)
            {
                return NotFound();
            }

            return producto_Categoria;
        }

        // PUT: api/Producto_CategoriasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto_Categoria(int id, Producto_Categoria producto_Categoria)
        {
            if (id != producto_Categoria.ID)
            {
                return BadRequest();
            }

            _context.Entry(producto_Categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Producto_CategoriaExists(id))
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

        // POST: api/Producto_CategoriasApi
        [HttpPost]
        public async Task<ActionResult<Producto_Categoria>> PostProducto_Categoria(Producto_Categoria producto_Categoria)
        {
            _context.Producto_Categorias.Add(producto_Categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto_Categoria", new { id = producto_Categoria.ID }, producto_Categoria);
        }

        // DELETE: api/Producto_CategoriasApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto_Categoria>> DeleteProducto_Categoria(int id)
        {
            var producto_Categoria = await _context.Producto_Categorias.FindAsync(id);
            if (producto_Categoria == null)
            {
                return NotFound();
            }

            _context.Producto_Categorias.Remove(producto_Categoria);
            await _context.SaveChangesAsync();

            return producto_Categoria;
        }

        private bool Producto_CategoriaExists(int id)
        {
            return _context.Producto_Categorias.Any(e => e.ID == id);
        }
    }
}
