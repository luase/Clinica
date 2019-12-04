namespace Clinica.Web.Data
{
    using System.Linq;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class PacientRepository : GenericRepository<Pacients>, IPacientRepository
    {
        private readonly DataContext context;

        public PacientRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Pacients.Include(p => p.User);
        }
    }

}
