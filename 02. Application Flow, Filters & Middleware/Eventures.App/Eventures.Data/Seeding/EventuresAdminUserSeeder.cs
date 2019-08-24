namespace Eventures.Data.Seeding
{
    using Eventures.Domain;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class EventuresAdminUserSeeder : ISeeder
    {
        private UserManager<EventuresUser> userManager;

        public EventuresAdminUserSeeder(UserManager<EventuresUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            var admin = new EventuresUser
            {
                UserName = "admin",
                Email = "admin@email.com",
                FirstName = "Admin",
                LastName = "Admin",
                UCN = "12345678910"
            };

            var result = await this.userManager.CreateAsync(admin, "admin");
            await this.userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
