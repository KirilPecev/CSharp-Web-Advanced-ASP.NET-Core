namespace Eventures.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventuresUserRoleSeeder : ISeeder
    {
        private readonly EventuresDbContext context;

        public EventuresUserRoleSeeder(EventuresDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (!this.context.Roles.Any())
            {
                this.context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                this.context.Roles.Add(new IdentityRole { Name = "User", NormalizedName = "USER" });

                await this.context.SaveChangesAsync();
            }
        }
    }
}
