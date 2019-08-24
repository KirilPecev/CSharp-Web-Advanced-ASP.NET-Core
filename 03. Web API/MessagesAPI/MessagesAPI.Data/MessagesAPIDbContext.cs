namespace MessagesAPI.Data
{
    using MessagesAPI.Domain;
    using Microsoft.EntityFrameworkCore;

    public class MessagesAPIDbContext : DbContext
    {
        public MessagesAPIDbContext(DbContextOptions<MessagesAPIDbContext> options)
            : base(options) { }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
