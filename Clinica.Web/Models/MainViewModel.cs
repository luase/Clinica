namespace Clinica.Web.Models
{
    using Clinica.Web.Data.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class PacientViewModel : Pacients
    {
        [Display(Name = "Picture")]
        public IFormFile PictureFile { get; set; }
    }

}
