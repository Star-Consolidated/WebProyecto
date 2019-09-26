using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProyecto.Data;
using WebProyecto.Models;
using Microsoft.AspNetCore.Authorization;


namespace WebProyecto.Controllers
{
    [Authorize]
    public class Producto_CategoriasController : Controller
    {
        private readonly ShopContext _context;

        public Producto_CategoriasController(ShopContext context)
        {
            _context = context;
        }

        // GET: Producto_Categorias
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Producto_Categorias.Include(p => p.Categoria).Include(p => p.Producto);
            return View(await shopContext.ToListAsync());
        }

        // GET: Producto_Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto_Categoria = await _context.Producto_Categorias
                .Include(p => p.Categoria)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (producto_Categoria == null)
            {
                return NotFound();
            }

            return View(producto_Categoria);
        }

        // GET: Producto_Categorias/Create
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "ID", "ID");
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID");
            return View();
        }

        // POST: Producto_Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductoID,CategoriaID")] Producto_Categoria producto_Categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto_Categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "ID", "ID", producto_Categoria.CategoriaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", producto_Categoria.ProductoID);
            return View(producto_Categoria);
        }

        // GET: Producto_Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto_Categoria = await _context.Producto_Categorias.FindAsync(id);
            if (producto_Categoria == null)
            {
                return NotFound();
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "ID", "ID", producto_Categoria.CategoriaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", producto_Categoria.ProductoID);
            return View(producto_Categoria);
        }

        // POST: Producto_Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductoID,CategoriaID")] Producto_Categoria producto_Categoria)
        {
            if (id != producto_Categoria.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto_Categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Producto_CategoriaExists(producto_Categoria.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorias, "ID", "ID", producto_Categoria.CategoriaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", producto_Categoria.ProductoID);
            return View(producto_Categoria);
        }

        // GET: Producto_Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto_Categoria = await _context.Producto_Categorias
                .Include(p => p.Categoria)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (producto_Categoria == null)
            {
                return NotFound();
            }

            return View(producto_Categoria);
        }

        // POST: Producto_Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto_Categoria = await _context.Producto_Categorias.FindAsync(id);
            _context.Producto_Categorias.Remove(producto_Categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Producto_CategoriaExists(int id)
        {
            return _context.Producto_Categorias.Any(e => e.ID == id);
        }
    }
}
