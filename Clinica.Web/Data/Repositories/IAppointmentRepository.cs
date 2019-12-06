namespace Clinica.Web.Data.Repositories
{
    using Clinica.Web.Models;
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAppointmentAsync(string userName);

        Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync (string userName);

        Task AddItemToPacientAsync(AddItemViewModel model, string userName);

        Task DeleteDetailTempAsync(int id);

        Task<bool> ConfirmOrderAsync(string userName);



    }
}
