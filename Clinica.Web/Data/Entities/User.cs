namespace Clinica.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        //TODO: Agregar campos
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display (Name ="Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }
    }
}
