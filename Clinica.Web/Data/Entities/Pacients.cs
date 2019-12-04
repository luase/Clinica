namespace Clinica.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pacients : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} puede contener un máximo de {1} caracteres")]
        [Required]

        public string Name { get; set; }
        
        public string Address { get; set; }

        // [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        // public decimal Price { get; set; }

        [Display(Name = "Picture")]
        public string PictureUrl { get; set; }

        // [Display(Name = "Last Purchase")]
        // public DateTime LastPurchase { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }


        // [Display(Name = "Is Availabe?")]
        // public bool IsAvailabe { get; set; }

        // [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        // public double Stock { get; set; }

        public User User { get; set; }

        public string ImageFullPath {
            get
            {
                if (string.IsNullOrEmpty(this.PictureUrl))
                {
                    return null;
                }

                return $"https://clinicaapp.azurewebsites.net{this.PictureUrl.Substring(1)}";
            } 
        }
    }

}
