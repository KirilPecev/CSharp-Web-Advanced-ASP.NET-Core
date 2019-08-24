namespace Eventures.App.Extensions
{
    using Eventures.Data;
    using Eventures.Data.Seeding;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;

    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Assembly.GetAssembly(typeof(EventuresDbContext))
                    .GetTypes()
                    .Where(t => typeof(ISeeder).IsAssignableFrom(t))
                    .Where(t => t.IsClass)
                    .Select(t => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(t))
                    .ToList()
                    .ForEach(async seeder => await seeder.SeedAsync());
            }
        }
    }
}
