﻿namespace Clinica.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Clinica.Web.Models;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, UserPro, Doctor")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, UserPro, Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacientViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.PictureFile != null && view.PictureFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";


                    path = Path.Combine(
                        Directory.GetCurrentDirectory(), 
                        "wwwroot\\images\\Pacients", 
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.PictureFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Pacients/{file}";
                }

                var pacient = this.ToPacient(view, path);
                pacient.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
        [Authorize(Roles = "Admin, UserPro, Doctor")]
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
        [Authorize(Roles = "Admin, UserPro, Doctor")]
        public async Task<IActionResult> Edit(PacientViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    if (view.PictureFile != null && view.PictureFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";


                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Pacients",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.PictureFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Pacients/{file}";
                    }

                    var pacient = this.ToPacient(view, path);
                    pacient.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
        [Authorize(Roles = "Admin, UserPro, Doctor")]
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
        [Authorize(Roles = "Admin, UserPro, Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacient = await this.pacientRepository.GetByIdAsync(id);
            await this.pacientRepository.DeleteAsync(pacient);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductNotFound()
        {
            return this.View();
        }
    }


}
