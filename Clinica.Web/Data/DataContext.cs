namespace Clinica.Web.Data
{
    using Clinica.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DbSet<Pacients> Pacients { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

}
