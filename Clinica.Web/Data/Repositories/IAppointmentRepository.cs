namespace Clinica.Web.Data.Repositories
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAppointmentAsync(string userName);

        Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync (string userName);

    }
}
