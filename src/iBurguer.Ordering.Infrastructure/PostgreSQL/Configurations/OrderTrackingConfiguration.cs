using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Ordering.Infrastructure.PostgreSQL.Configurations;

[ExcludeFromCodeCoverage]
public class OrderTrackingConfiguration : IEntityTypeConfiguration<OrderTracking>
{
    public void Configure(EntityTypeBuilder<OrderTracking> builder)
    {
        builder.ToTable("OrderTrackings");
        
        builder.HasKey(c => c.TrackingId);

        builder.Property(c => c.TrackingId).IsRequired().ValueGeneratedNever();
        
        builder.Property(c => c.When).IsRequired();
        
        builder.Property(c => c.OrderStatus)
               .HasConversion(status => status.Name, value => OrderStatus.FromName(value, true))
               .HasColumnName("OrderStatus")
               .IsRequired();
    }
}