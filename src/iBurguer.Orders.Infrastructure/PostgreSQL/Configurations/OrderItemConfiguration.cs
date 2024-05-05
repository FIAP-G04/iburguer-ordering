using iBurguer.Orders.Core.Domain;
using iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Orders.Infrastructure.PostgreSQL.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id).IsRequired();
        builder.Property(c => c.ProductId).IsRequired();
        builder.Property(c => c.ProductName).IsRequired();
        builder.Property(c => c.ProductType).IsEnum().IsRequired();
        builder.Property(c => c.UnitPrice).IsMoney().HasColumnName("UnitPrice").IsRequired();
        builder.Property(o => o.Quantity).IsQuantity().HasColumnName("Quantity").IsRequired();
    }
}


