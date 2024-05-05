using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.PostgreSQL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace iBurguer.Orders.Infrastructure.PostgreSQL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) =>
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderTracking> Trackings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.EnableServiceProviderCaching(false);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Money>().HasNoKey();
        
        modelBuilder.HasSequence<int>("sq_order_number").IncrementsBy(1).HasMax(10000000).StartsAt(1).IsCyclic();

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderTrackingConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}