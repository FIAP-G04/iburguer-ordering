using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Ordering.Infrastructure.PostgreSQL.Configurations;

[ExcludeFromCodeCoverage]
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id).IsRequired();
        
        builder.Property(c => c.ProductId).IsRequired();
        
        builder.Property(c => c.ProductName).IsRequired();

        builder.Property(c => c.ProductType)
               .HasConversion(type => type.Name, value => ProductType.FromName(value, true))
               .HasColumnName("ProductType")
               .IsRequired();
        
        builder.Property(c => c.UnitPrice).IsMoney().HasColumnName("UnitPrice").IsRequired();
        
        builder.Property(o => o.Quantity).IsQuantity().HasColumnName("Quantity").IsRequired();
    }
}


