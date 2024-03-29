﻿namespace Clinica.Web.Controllers.API
{
    using Clinica.Web.Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PacientsController : Controller
    {
        private readonly IPacientRepository pacientRepository;

        public PacientsController(IPacientRepository pacientRepository)
        {
            this.pacientRepository = pacientRepository;
        }

        [HttpGet]
        public IActionResult GetPacients()
        {
            return this.Ok(this.pacientRepository.GetAllWithUsers());
        }
    }

}
