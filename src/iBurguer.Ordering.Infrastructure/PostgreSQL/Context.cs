using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace iBurguer.Ordering.Infrastructure.PostgreSQL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder) =>
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.EnableServiceProviderCaching(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("sq_order_number").IncrementsBy(1).HasMax(10000000).StartsAt(1).IsCyclic();

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderTrackingConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}