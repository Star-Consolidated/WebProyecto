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
    public class CaracteristicasController : Controller
    {
        private readonly ShopContext _context;

        public CaracteristicasController(ShopContext context)
        {
            _context = context;
        }

        // GET: Caracteristicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Caracteristicas.ToListAsync());
        }

        // GET: Caracteristicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristica = await _context.Caracteristicas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            return View(caracteristica);
        }

        // GET: Caracteristicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caracteristicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre")] Caracteristica caracteristica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caracteristica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caracteristica);
        }

        // GET: Caracteristicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            if (caracteristica == null)
            {
                return NotFound();
            }
            return View(caracteristica);
        }

        // POST: Caracteristicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre")] Caracteristica caracteristica)
        {
            if (id != caracteristica.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caracteristica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaracteristicaExists(caracteristica.ID))
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
            return View(caracteristica);
        }

        // GET: Caracteristicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristica = await _context.Caracteristicas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            return View(caracteristica);
        }

        // POST: Caracteristicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            _context.Caracteristicas.Remove(caracteristica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaracteristicaExists(int id)
        {
            return _context.Caracteristicas.Any(e => e.ID == id);
        }
    }
}
