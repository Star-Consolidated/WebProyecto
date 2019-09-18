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
    public class Detalle_PedidosController : Controller
    {
        private readonly ShopContext _context;

        public Detalle_PedidosController(ShopContext context)
        {
            _context = context;
        }

        // GET: Detalle_Pedidos
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Detalle_Pedidos.Include(d => d.Carrito).Include(d => d.Pedido);
            return View(await shopContext.ToListAsync());
        }

        // GET: Detalle_Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Pedido = await _context.Detalle_Pedidos
                .Include(d => d.Carrito)
                .Include(d => d.Pedido)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detalle_Pedido == null)
            {
                return NotFound();
            }

            return View(detalle_Pedido);
        }

        // GET: Detalle_Pedidos/Create
        public IActionResult Create()
        {
            ViewData["CarritoID"] = new SelectList(_context.Carritos, "ID", "ID");
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "ID", "ID");
            return View();
        }

        // POST: Detalle_Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CarritoProductoID,CarritoUsuarioID,CarritoID,PedidoID")] Detalle_Pedido detalle_Pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle_Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoID"] = new SelectList(_context.Carritos, "ID", "ID", detalle_Pedido.CarritoID);
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "ID", "ID", detalle_Pedido.PedidoID);
            return View(detalle_Pedido);
        }

        // GET: Detalle_Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Pedido = await _context.Detalle_Pedidos.FindAsync(id);
            if (detalle_Pedido == null)
            {
                return NotFound();
            }
            ViewData["CarritoID"] = new SelectList(_context.Carritos, "ID", "ID", detalle_Pedido.CarritoID);
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "ID", "ID", detalle_Pedido.PedidoID);
            return View(detalle_Pedido);
        }

        // POST: Detalle_Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CarritoProductoID,CarritoUsuarioID,CarritoID,PedidoID")] Detalle_Pedido detalle_Pedido)
        {
            if (id != detalle_Pedido.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle_Pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Detalle_PedidoExists(detalle_Pedido.ID))
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
            ViewData["CarritoID"] = new SelectList(_context.Carritos, "ID", "ID", detalle_Pedido.CarritoID);
            ViewData["PedidoID"] = new SelectList(_context.Pedidos, "ID", "ID", detalle_Pedido.PedidoID);
            return View(detalle_Pedido);
        }

        // GET: Detalle_Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle_Pedido = await _context.Detalle_Pedidos
                .Include(d => d.Carrito)
                .Include(d => d.Pedido)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (detalle_Pedido == null)
            {
                return NotFound();
            }

            return View(detalle_Pedido);
        }

        // POST: Detalle_Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle_Pedido = await _context.Detalle_Pedidos.FindAsync(id);
            _context.Detalle_Pedidos.Remove(detalle_Pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Detalle_PedidoExists(int id)
        {
            return _context.Detalle_Pedidos.Any(e => e.ID == id);
        }
    }
}
