namespace Clinica.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("UserPro");
            await this.userHelper.CheckRoleAsync("Doctor");


            var user = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Juan",
                    LastName = "Zuluaga",
                    Email = "jzuluaga55@gmail.com",
                    UserName = "jzuluaga55@gmail.com"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }



            if (!this.context.Pacients.Any())
            {
                this.AddProduct("Pedro Perez",user);
                this.AddProduct("Diana García", user);
                this.AddProduct("Miguel Rodriguez", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _ = this.context.Pacients.Add(new Pacients
            {
                Name = name,
                Address = "Una calle 20",
                User = user
            });
        }
    }

}
