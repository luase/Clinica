namespace Clinica.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<SelectListItem> GetComboPacients()
        {
            var list = this.context.Pacients.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a patient...)",
                Value = "0"
            });

            return list;
        }


    }

}
