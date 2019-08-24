namespace FluffyDuffyMunchkinCats.Models.Data
{
    using Microsoft.EntityFrameworkCore;

    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public DbSet<Cat> Cats { get; set; }
    }
}
