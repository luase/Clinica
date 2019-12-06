namespace Clinica.Web.Data
{
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public interface IPacientRepository : IGenericRepository<Pacients>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboPacients();

    }


}
