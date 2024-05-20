using System.Diagnostics.CodeAnalysis;
using Ardalis.SmartEnum;
using iBurguer.Ordering.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iBurguer.Ordering.Infrastructure.PostgreSQL.Extensions;

[ExcludeFromCodeCoverage]
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
}