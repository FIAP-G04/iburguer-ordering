using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Orders.Infrastructure.PostgreSQL.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).IsRequired();

        builder.Property(o => o.Number)
            .HasConversion(
                orderNumber => orderNumber.Value,
                number => new OrderNumber(number))
            .HasColumnName("OrderNumber")
            .IsRequired();

        builder.Property(o => o.PickupCode)
            .HasConversion(
                pickupCode => pickupCode.Code,
                code => new PickupCode(code))
            .HasColumnName("PickupCode")
            .IsRequired();
        
        builder.Property(c => c.CreatedAt)
            .IsRequired();
    
        builder.Property(c => c.Type)
            .HasConversion(
                type => type.ToString(),
                name => OrderType.FromName(name))
            .HasColumnName("OrderType")
            .IsRequired();
    
        builder.Property(c => c.PaymentMethod).IsEnum()
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