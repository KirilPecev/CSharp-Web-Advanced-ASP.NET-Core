namespace Eventures.Data
{
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EventuresDbContext : IdentityDbContext<EventuresUser, IdentityRole, string>
    {
        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
        : base(options) { }

        public DbSet<Event> Events { get; set; }
    }
}
