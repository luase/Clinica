namespace Clinica.Web.Data.Entities
{
    
    using System.ComponentModel.DataAnnotations;

    public class AppointmentDetailTemp : IEntity
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Pacients Pacient { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }

        public string Illness { get; set; }

        public string Treatment { get; set; }

        public string Comments { get; set; }
    }

}
