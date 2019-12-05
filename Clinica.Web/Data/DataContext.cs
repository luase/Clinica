namespace Clinica.Web.Data
{
    using Clinica.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Pacients> Pacients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

        public DbSet<AppointmentDetailTemp> AppointmentDetailTemps { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Falta agregar override de un warning video 25 2:47 min
    }

}
