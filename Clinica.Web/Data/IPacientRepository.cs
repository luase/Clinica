namespace Clinica.Web.Data
{
    using Entities;
    using System.Linq;

    public interface IPacientRepository : IGenericRepository<Pacients>
    {
        IQueryable GetAllWithUsers();
    }


}
