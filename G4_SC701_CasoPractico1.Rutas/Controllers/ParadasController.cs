using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G4_SC701_CasoPractico1.Rutas.Models;

namespace G4_SC701_CasoPractico1.Rutas.Controllers
{
    public class ParadasController : Controller
    {
        private readonly CP1Context _context;

        public ParadasController(CP1Context context)
        {
            _context = context;
        }

        // GET: Paradas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paradas.ToListAsync());
        }

        // GET: Paradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paradas = await _context.Paradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paradas == null)
            {
                return NotFound();
            }

            return View(paradas);
        }

        // GET: Paradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Paradas paradas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paradas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paradas);
        }

        // GET: Paradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paradas = await _context.Paradas.FindAsync(id);
            if (paradas == null)
            {
                return NotFound();
            }
            return View(paradas);
        }

        // POST: Paradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Paradas paradas)
        {
            if (id != paradas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paradas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParadasExists(paradas.Id))
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
            return View(paradas);
        }

        // GET: Paradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paradas = await _context.Paradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paradas == null)
            {
                return NotFound();
            }

            return View(paradas);
        }

        // POST: Paradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paradas = await _context.Paradas.FindAsync(id);
            if (paradas != null)
            {
                _context.Paradas.Remove(paradas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParadasExists(int id)
        {
            return _context.Paradas.Any(e => e.Id == id);
        }
    }
}
