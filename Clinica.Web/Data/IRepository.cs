
namespace Clinica.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository
    {
        void AddPacient(Pacients pacient);

        Pacients GetPacient(int id);

        IEnumerable<Pacients> GetPacients();

        bool PacientExists(int id);

        void RemovePacient(Pacients pacient);

        Task<bool> SaveAllAsync();

        void UpdatePacient(Pacients pacient);
    }
}