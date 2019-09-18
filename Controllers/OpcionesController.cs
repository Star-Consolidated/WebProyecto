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
    public class OpcionesController : Controller
    {
        private readonly ShopContext _context;

        public OpcionesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Opciones
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Opciones.Include(o => o.Caracteristica);
            return View(await shopContext.ToListAsync());
        }

        // GET: Opciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcion = await _context.Opciones
                .Include(o => o.Caracteristica)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (opcion == null)
            {
                return NotFound();
            }

            return View(opcion);
        }

        // GET: Opciones/Create
        public IActionResult Create()
        {
            ViewData["CaracteristicaID"] = new SelectList(_context.Caracteristicas, "ID", "ID");
            return View();
        }

        // POST: Opciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,CaracteristicaID")] Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaracteristicaID"] = new SelectList(_context.Caracteristicas, "ID", "ID", opcion.CaracteristicaID);
            return View(opcion);
        }

        // GET: Opciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcion = await _context.Opciones.FindAsync(id);
            if (opcion == null)
            {
                return NotFound();
            }
            ViewData["CaracteristicaID"] = new SelectList(_context.Caracteristicas, "ID", "ID", opcion.CaracteristicaID);
            return View(opcion);
        }

        // POST: Opciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,CaracteristicaID")] Opcion opcion)
        {
            if (id != opcion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcionExists(opcion.ID))
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
            ViewData["CaracteristicaID"] = new SelectList(_context.Caracteristicas, "ID", "ID", opcion.CaracteristicaID);
            return View(opcion);
        }

        // GET: Opciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcion = await _context.Opciones
                .Include(o => o.Caracteristica)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (opcion == null)
            {
                return NotFound();
            }

            return View(opcion);
        }

        // POST: Opciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opcion = await _context.Opciones.FindAsync(id);
            _context.Opciones.Remove(opcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcionExists(int id)
        {
            return _context.Opciones.Any(e => e.ID == id);
        }
    }
}
