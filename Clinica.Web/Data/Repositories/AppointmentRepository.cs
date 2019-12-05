namespace Clinica.Web.Data.Repositories
{
    using Clinica.Web.Helpers;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public AppointmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task<IQueryable<Appointment>> GetAppointmentAsync(string userName)
        {
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            //Explicado Orders Functionality V32
            //TODO: Agregar Illness, treatment, comments, price
            if (await this.userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return this.context.Appointments
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Pacient)
                    .OrderByDescending(o => o.Date);
            }
            //TODO: Agregar price
            if (await this.userHelper.IsUserInRoleAsync(user, "Doctor"))
            {
                return this.context.Appointments
                .Include(o => o.Items)
                .ThenInclude(i => i.Pacient)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.Date);
            }

            
            return this.context.Appointments
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(i => i.Pacient)
            .OrderByDescending(o => o.Date);
            

        }

        public async Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }


            return this.context.AppointmentDetailTemps
            .Include(o => o.Pacient)
            .Include(o => o.Value)
            .Where(i => i.User == user)
            .OrderByDescending(o => o.Pacient.Name);


        }
    }
    
}
