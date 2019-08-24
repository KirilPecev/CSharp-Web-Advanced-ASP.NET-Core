namespace Panda.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PandaDbContext : IdentityDbContext<PandaUser, PandaUserRole, string>
    {
        public PandaDbContext(DbContextOptions<PandaDbContext> options) :
            base(options)
        {

        }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PandaUser>()
                .HasMany(x => x.Packages)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);

            builder.Entity<PandaUser>()
                .HasMany(x => x.Receipts)
                .WithOne(x => x.Recipient)
                .HasForeignKey(x => x.RecipientId);

            builder.Entity<Receipt>()
                .HasOne(p => p.Package)
                .WithOne(r => r.Receipt)
                .HasForeignKey<Receipt>(x => x.PackageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
