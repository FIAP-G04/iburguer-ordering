using iBurguer.Orders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Orders.Infrastructure.PostgreSQL.Extensions;

public static class PropertyBuilderExtensions
{

    public static PropertyBuilder<T> IsEnum<T>(this PropertyBuilder<T> propertyBuilder) =>
        propertyBuilder.HasConversion(
            enumeration => enumeration!.ToString(),
            value => (T)Enum.Parse(typeof(T), value));

    public static PropertyBuilder<Money> IsMoney(this PropertyBuilder<Money> propertyBuilder) =>
        propertyBuilder.HasConversion(
            price => price.Amount,
            value => new Money(value)).HasColumnType("money");
    
    public static PropertyBuilder<Quantity> IsQuantity(this PropertyBuilder<Quantity> propertyBuilder) =>
        propertyBuilder.HasConversion(
            quantity => quantity.Value,
            value => new Quantity(value));
    
    public static PropertyBuilder<OrderType> IsOrderType(this PropertyBuilder<OrderType> propertyBuilder) =>
        propertyBuilder.HasConversion(
            type => type.ToString(),
            value => OrderType.FromName(value));
    
    public static PropertyBuilder<OrderType> IsOrderType(this PropertyBuilder<OrderType> propertyBuilder) =>
        propertyBuilder.HasConversion(
            type => type.ToString(),
            value => OrderType.FromName(value));
}