using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using G4_SC701_CasoPractico1.Rutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G4_SC701_CasoPractico1.Rutas.Controllers
{
    [Route("api/[controller]")]
    public class BoletoController : Controller
    {
        private readonly CP1Context _context;

        public BoletoController(CP1Context context)
        {
            _context = context;
        }

        // GET: api/Boleto
        public async Task<IActionResult> Index()
        {
            var boletos = await _context.Boletos
                .Include(b => b.ruta)
                .ThenInclude(r => r.vehiculo) // Incluye la relación con Vehiculo
                .Include(b => b.usuario)
                .ToListAsync();

            return View(boletos);
        }

        // GET: api/Boleto/5
        public async Task<IActionResult> Details(int id)
        {
            var boleto = await _context.Boletos
                .Include(b => b.ruta)
                .ThenInclude(r => r.vehiculo)
                .Include(b => b.usuario)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // GET: Boleto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boleto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Boleto boleto)
        {
            if (!ModelState.IsValid)
            {
                return View(boleto);
            }

            // Validar si la ruta existe e incluir el vehículo
            var ruta = await _context.Rutas
                .Include(r => r.vehiculo) // Incluir la relación con Vehiculo
                .FirstOrDefaultAsync(r => r.Id == boleto.IdRuta);

            if (ruta == null)
            {
                ModelState.AddModelError("", "Ruta no encontrada");
                return View(boleto);
            }

            // Validar disponibilidad de asientos con la capacidad del vehículo
            int boletosVendidos = await _context.Boletos.CountAsync(b => b.IdRuta == boleto.IdRuta);
            if (boletosVendidos >= ruta.vehiculo.CapacidadPasajeros) // Ahora usa CapacidadPasajeros de Vehiculo
            {
                ModelState.AddModelError("", "No hay asientos disponibles para esta ruta");
                return View(boleto);
            }

            boleto.FechaCompra = DateTime.UtcNow;
            _context.Boletos.Add(boleto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Boleto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // POST: Boleto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }

            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
