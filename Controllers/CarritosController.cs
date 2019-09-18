using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProyecto.Data;
using WebProyecto.Models;

namespace WebProyecto.Controllers
{
    public class CarritosController : Controller
    {
        private readonly ShopContext _context;

        public CarritosController(ShopContext context)
        {
            _context = context;
        }

        // GET: Carritos
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Carritos.Include(c => c.Producto).Include(c => c.Usuario);
            return View(await shopContext.ToListAsync());
        }

        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID");
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cantidad,SubTotal,ProductoID,UsuarioID,ID")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", carrito.ProductoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", carrito.UsuarioID);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", carrito.ProductoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", carrito.UsuarioID);
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cantidad,SubTotal,ProductoID,UsuarioID,ID")] Carrito carrito)
        {
            if (id != carrito.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.ID))
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
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ID", "ID", carrito.ProductoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", carrito.UsuarioID);
            return View(carrito);
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.ID == id);
        }
    }
}
