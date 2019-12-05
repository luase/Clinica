namespace Clinica.Web.Data.Entities
{

    using System.ComponentModel.DataAnnotations;

    public class AppointmentDetail : IEntity
    {
        public int Id { get; set; }

        [Required]
        public Pacients Pacient { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        public string Illness { get; set; }

        public string Treatment { get; set; }

        public string Comments { get; set; }
    }

}
