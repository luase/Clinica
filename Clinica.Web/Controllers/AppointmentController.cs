namespace Clinica.Web.Controllers
{
    using Clinica.Web.Data.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models;
    using Clinica.Web.Data;

    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPacientRepository pacientRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, IPacientRepository pacientRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.pacientRepository = pacientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await appointmentRepository.GetAppointmentAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await this.appointmentRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }

        public IActionResult AddPacient()
        {
            var model = new AddItemViewModel
            {
                Pacients = this.pacientRepository.GetComboPacients()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPacient(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.appointmentRepository.AddItemToPacientAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.appointmentRepository.DeleteDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await this.appointmentRepository.ConfirmOrderAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }


    }
}
