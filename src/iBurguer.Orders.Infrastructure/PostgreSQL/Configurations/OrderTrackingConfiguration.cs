using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Orders.Infrastructure.PostgreSQL.Configurations;

public class OrderTrackingConfiguration : IEntityTypeConfiguration<OrderTracking>
{
    public void Configure(EntityTypeBuilder<OrderTracking> builder)
    {
        builder.ToTable("OrderTrackings");
        
        builder.HasKey(c => c.TrackingId);
        
        builder.Property(c => c.TrackingId).IsRequired();
        builder.Property(c => c.OrderStatus).IsEnum().IsRequired();
        builder.Property(c => c.When).IsRequired();
    }
}