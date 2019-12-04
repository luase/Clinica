namespace Clinica.Web.Data
{
    using Entities;

    public class PacientRepository : GenericRepository<Pacients>, IPacientRepository
    {
        public PacientRepository(DataContext context) : base(context)
        {
        }
    }

}
