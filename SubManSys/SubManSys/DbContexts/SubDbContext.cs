using Microsoft.EntityFrameworkCore;
using SubManSys.Models;

namespace SubManSys.DbContexts;

public class SubDbContext : DbContext
{
    public DbSet<Client> Client { get; set; }
    public DbSet<Discount> Discount { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<Sale> Sale { get; set; }
    public DbSet<Subscription> Subscription { get; set; }

    public SubDbContext(DbContextOptions<SubDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasKey(c => c.IdClient);
        modelBuilder.Entity<Discount>().HasKey(c => c.IdDiscount);
        modelBuilder.Entity<Payment>().HasKey(c => c.IdPayment);
        modelBuilder.Entity<Sale>().HasKey(c => c.IdSale);
        modelBuilder.Entity<Subscription>().HasKey(c => c.IdSubscription);
    }
}