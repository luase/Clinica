namespace Clinica.Web.Data
{
    using Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Pacients> GetPacients()
        {
            return this.context.Pacients.OrderBy(p => p.Name);
        }

        public Pacients GetPacient(int id)
        {
            return this.context.Pacients.Find(id);
        }

        public void AddPacient(Pacients pacients)
        {
            this.context.Pacients.Add(pacients);
        }

        public void UpdatePacient(Pacients pacients)
        {
            this.context.Update(pacients);
        }

        public void RemovePacient(Pacients pacients)
        {
            this.context.Pacients.Remove(pacients);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool PacientExists(int id)
        {
            return this.context.Pacients.Any(p => p.Id == id);
        }
    }
}
