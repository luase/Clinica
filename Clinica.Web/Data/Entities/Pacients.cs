﻿namespace Clinica.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pacients
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }

        // [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        // public decimal Price { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        // [Display(Name = "Last Purchase")]
        // public DateTime LastPurchase { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        // [Display(Name = "Is Availabe?")]
        // public bool IsAvailabe { get; set; }

        // [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        // public double Stock { get; set; }
    }

}