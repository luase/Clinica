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
        private readonly IRepository repository;

        public PacientsController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: Pacients
        public IActionResult Index()
        {
            return View(this.repository.GetPacients());
        }

        // GET: Pacients/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = this.repository.GetPacient(id.Value);
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
        public async Task<IActionResult> Create(Pacients pacients)
        {
            if (ModelState.IsValid)
            {
                this.repository.AddPacient(pacients);
                await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pacients);
        }

        // GET: Pacients/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = this.repository.GetPacient(id.Value);
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
        public async Task<IActionResult> Edit(Pacients pacients)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.repository.UpdatePacient(pacients);
                    await this.repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.PacientExists(pacients.Id))
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacients = this.repository.GetPacient(id.Value);
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
            var pacients = this.repository.GetPacient(id);
            this.repository.RemovePacient(pacients);
            await this.repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
