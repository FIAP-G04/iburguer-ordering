using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Ordering.Infrastructure.PostgreSQL.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).IsRequired();

        builder.Property(o => o.Number)
               .HasConversion(number => number.Value, value => new OrderNumber(value))
               .HasColumnName("OrderNumber")
               .IsRequired();
        
        builder.Property(o => o.PickupCode)
               .HasConversion(pickupCode => pickupCode.Code, value => new PickupCode(value))
               .HasColumnName("PickupCode")
               .IsRequired();
        
        builder.Property(c => c.CreatedAt).IsRequired();
        
        builder.Property(o => o.Type)
               .HasConversion(type => type.Name, value => OrderType.FromName(value, true))
               .HasColumnName("OrderType")
               .IsRequired();
    
        builder.Property(o => o.PaymentMethod)
            .HasConversion(method => method.Name, value => PaymentMethod.FromName(value, true))
            .HasColumnName("PaymentMethod")
            .IsRequired();

        builder.Property(c => c.BuyerId);
        
        builder.HasMany(p => p.Trackings)
            .WithOne(o => o.Order)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}