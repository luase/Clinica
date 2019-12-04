namespace Clinica.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Clinica.Web.Models;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PacientsController : Controller
    {
        private readonly IPacientRepository pacientRepository;

        private readonly IUserHelper userHelper;

        public PacientsController(IPacientRepository pacientRepository, IUserHelper userHelper)
        {
            this.pacientRepository = pacientRepository;
            this.userHelper = userHelper;
        }

        // GET: Pacients
        public IActionResult Index()
        {
            return View(this.pacientRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Pacients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await this.pacientRepository.GetByIdAsync(id.Value);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // GET: Pacients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacientViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.PictureFile != null && view.PictureFile.Length > 0)
                {
                    path = Path.Combine(
                        Directory.GetCurrentDirectory(), 
                        "wwwroot\\images\\Pacients", 
                        view.PictureFile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.PictureFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Pacients/{view.PictureFile.FileName}";
                }

                var pacient = this.ToPacient(view, path);
                // TODO: Pending to change to: this.User.Identity.Name
                pacient.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                await this.pacientRepository.CreateAsync(pacient);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Pacients ToPacient(PacientViewModel view, string path)
        {
            return new Pacients
            {
                Id = view.Id,
                Name = view.Name,
                Address = view.Address,
                BirthDate = view.BirthDate,
                PictureUrl = path,
                User = view.User
            };
        }

        // GET: Pacients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await this.pacientRepository.GetByIdAsync(id.Value);
            if (pacient == null)
            {
                return NotFound();
            }

            var view = this.ToPacientViewModel(pacient);
            return View(view);
        }

        private object ToPacientViewModel(Pacients pacient)
        {
            return new PacientViewModel
            {
                Id = pacient.Id,
                Name = pacient.Name,
                Address = pacient.Address,
                BirthDate = pacient.BirthDate,
                PictureUrl=pacient.PictureUrl,
                User = pacient.User
            };
            
        }

        // POST: Pacients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PacientViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.PictureUrl;

                    if (view.PictureFile != null && view.PictureFile.Length > 0)
                    {
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Pacients",
                            view.PictureFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.PictureFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Pacients/{view.PictureFile.FileName}";
                    }

                    var pacient = this.ToPacient(view, path);
                    // TODO: Pending to change to: this.User.Identity.Name
                    pacient.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                    await this.pacientRepository.UpdateAsync(pacient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.pacientRepository.ExistAsync(view.Id))
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

            return View(view);
        }

        // GET: Pacients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await this.pacientRepository.GetByIdAsync(id.Value);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacient = await this.pacientRepository.GetByIdAsync(id);
            await this.pacientRepository.DeleteAsync(pacient);
            return RedirectToAction(nameof(Index));
        }
    }


}
