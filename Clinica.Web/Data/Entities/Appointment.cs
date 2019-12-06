namespace Clinica.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Appointment : IEntity
    {
        public int Id { get; set; }


        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? Date { get; set; }

        [Required]
        public User User { get; set; } //Doctor

        public IEnumerable<AppointmentDetail> Items { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines { get { return this.Items == null ? 0 : this.Items.Count(); } }


        [Display(Name ="Illness")]
        public string Illness { get; set; }

        [Display(Name = "Treatment")]
        public string Treatment { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }


        //parte 35 4/4
        /*
        [Display(Name = "LocalDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? AppointmentDateLocal
        {
            get
            {
                if (this.Date == null)
                {
                    return null;
                }

                return this.Date.ToLocalTime();
            }
        }
        */

    }

}
