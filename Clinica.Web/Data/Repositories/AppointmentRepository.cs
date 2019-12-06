namespace Clinica.Web.Data.Repositories
{
    using Clinica.Web.Helpers;
    using Clinica.Web.Models;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly IPacientRepository pacientRepository;

        public AppointmentRepository(DataContext context, IUserHelper userHelper, IPacientRepository pacientRepository) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.pacientRepository = pacientRepository;
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
            if (await this.userHelper.IsUserInRoleAsync(user, "Admin") || await this.userHelper.IsUserInRoleAsync(user, "UserPro"))
            {
                return this.context.Appointments
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Pacient)
                    .OrderByDescending(o => o.Date);
            }
            //TODO: Agregar price
            //Rol==Doctor
            else 
            {
                return this.context.Appointments
                .Include(o => o.Items)
                .ThenInclude(i => i.Pacient)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.Date);
            }


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
            .Where(i => i.User == user)
            .OrderByDescending(o => o.Pacient.Name);


        }

        public async Task AddItemToPacientAsync(AddItemViewModel model, string userName)
        {
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var pacient = await this.context.Pacients.FindAsync(model.PacientId);
            if (pacient == null)
            {
                return;
            }

            var orderDetailTemp = await this.context.AppointmentDetailTemps
                .Where(odt => odt.User == user && odt.Pacient == pacient)
                .FirstOrDefaultAsync();
            if (orderDetailTemp == null)
            {
                orderDetailTemp = new AppointmentDetailTemp
                {
                    Pacient = pacient,
                    User = user,
                };

                this.context.AppointmentDetailTemps.Add(orderDetailTemp);
            }
            else
            {
                // Cuando un usuario ya pidió una cita para el mismo paciente
            }

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await this.context.AppointmentDetailTemps.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            this.context.AppointmentDetailTemps.Remove(orderDetailTemp);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await this.userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var orderTmps = await this.context.AppointmentDetailTemps
                .Include(o => o.Pacient)
                .Where(o => o.User == user)
                .ToListAsync();

            if (orderTmps == null || orderTmps.Count == 0)
            {
                return false;
            }

            var details = orderTmps.Select(o => new AppointmentDetail
            {
                Pacient = o.Pacient
            }).ToList();

            var order = new Appointment
            {
                User = user,
                Items = details,
            };

            this.context.Appointments.Add(order);
            this.context.AppointmentDetailTemps.RemoveRange(orderTmps);
            await this.context.SaveChangesAsync();
            return true;
        }


    }

}
