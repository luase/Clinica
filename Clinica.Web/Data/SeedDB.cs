namespace Clinica.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Clinica.Web.Data.Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Pacients.Any())
            {
                this.AddProduct("Pedro Perez");
                this.AddProduct("Diana García");
                this.AddProduct("Miguel Rodriguez");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _ = this.context.Pacients.Add(new Pacients
            {
                Name = name,
                Address = "Una calle 20"
            });
        }
    }

}
