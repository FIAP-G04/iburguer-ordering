using AutoFixture.Xunit2;
using FluentAssertions;
using iBurguer.Ordering.Core;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class OrderNumberTests
{
    [Theory, AutoData]
    public void ShouldCreateValidOrderNumber(int value)
    {
        // Act
        var orderNumber = new OrderNumber(value);

        // Assert
        orderNumber.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void OrderNumber_Value_InvalidValue_ThrowsException(int invalidValue)
    {
        // Act & Assert
        Assert.Throws<Exceptions.InvalidOrderNumberException>(() => new OrderNumber(invalidValue));
    }

    [Theory, AutoData]
    public void ShouldPerformImplicitConversionFromIntToOrderNumberCorrectly(int value)
    {
        // Act
        OrderNumber orderNumber = value;

        // Assert
        orderNumber.Value.Should().Be(value);
    }

    [Theory, AutoData]
    public void ShouldPerformImplicitConversionFromOrderNumberToIntCorrectly(int value)
    {
        // Arrange
        var orderNumber = new OrderNumber(value);

        // Act
        int result = orderNumber;

        // Assert
        result.Should().Be(orderNumber.Value);
    }
}