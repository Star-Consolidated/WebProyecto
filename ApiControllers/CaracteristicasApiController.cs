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
    [Route("api/Caracteristicas")]
    [ApiController]
    public class CaracteristicasApiController : ControllerBase
    {
        private readonly ShopContext _context;

        public CaracteristicasApiController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/CaracteristicasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caracteristica>>> GetCaracteristicas()
        {
            return await _context.Caracteristicas.ToListAsync();
        }
        ///<summary>
        /// </summary>
        // GET: api/CaracteristicasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Caracteristica>> GetCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);

            if (caracteristica == null)
            {
                return NotFound();
            }

            return caracteristica;
        }

        // PUT: api/CaracteristicasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristica(int id, Caracteristica caracteristica)
        {
            if (id != caracteristica.ID)
            {
                return BadRequest();
            }

            _context.Entry(caracteristica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaracteristicaExists(id))
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

        // POST: api/CaracteristicasApi
        [HttpPost]
        public async Task<ActionResult<Caracteristica>> PostCaracteristica(Caracteristica caracteristica)
        {
            _context.Caracteristicas.Add(caracteristica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristica", new { id = caracteristica.ID }, caracteristica);
        }

        // DELETE: api/CaracteristicasApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Caracteristica>> DeleteCaracteristica(int id)
        {
            var caracteristica = await _context.Caracteristicas.FindAsync(id);
            if (caracteristica == null)
            {
                return NotFound();
            }

            _context.Caracteristicas.Remove(caracteristica);
            await _context.SaveChangesAsync();

            return caracteristica;
        }

        private bool CaracteristicaExists(int id)
        {
            return _context.Caracteristicas.Any(e => e.ID == id);
        }
    }
}
