namespace Clinica.Web.Controllers
{
    using Clinica.Web.Data.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
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

    }
}
