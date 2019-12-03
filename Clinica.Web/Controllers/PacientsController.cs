using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Web.Data;
using Clinica.Web.Data.Entities;

namespace Clinica.Web.Controllers
{
    public class PacientsController : Controller
    {
        private readonly DataContext _context;

        public PacientsController(DataContext context)
        {
            _context = context;
        }

        // GET: Pacients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacients.ToListAsync());
        }

        // GET: Pacients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = await _context.Pacients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacients == null)
            {
                return NotFound();
            }

            return View(pacients);
        }

        // GET: Pacients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Picture,BirthDate")] Pacients pacients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacients);
        }

        // GET: Pacients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = await _context.Pacients.FindAsync(id);
            if (pacients == null)
            {
                return NotFound();
            }
            return View(pacients);
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Picture,BirthDate")] Pacients pacients)
        {
            if (id != pacients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientsExists(pacients.Id))
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
            return View(pacients);
        }

        // GET: Pacients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = await _context.Pacients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacients == null)
            {
                return NotFound();
            }

            return View(pacients);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacients = await _context.Pacients.FindAsync(id);
            _context.Pacients.Remove(pacients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientsExists(int id)
        {
            return _context.Pacients.Any(e => e.Id == id);
        }
    }
}
