using AutoFixture.Xunit2;
using FluentAssertions;
using iBurguer.Ordering.Core;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.UnitTests.Util;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class QuantityTests : BaseTests
{
    [Theory, AutoData]
    public void ShouldCreateValidQuantity(int value)
    {
        // Act
        var quantity = new Quantity(value);

        // Assert
        quantity.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void ShouldThrowAnExceptionWhenAInvalidValueIsProvided(int value)
    {
        // Act & Assert
        Assert.Throws<Exceptions.InvalidQuantityException>(() => new Quantity(value));
    }

    [Fact]
    public void ShouldReturnsTrueWhenTheQuntityIsMinimumValue()
    {
        // Arrange
        var quantity = new Quantity(1);

        // Act & Assert
        quantity.IsMinimum().Should().BeTrue();
    }
    
    [Fact]
    public void ShouldReturnsFalseWhenTheQuntityIsNotMinimumValue()
    {
        // Arrange
        var quantity = new Quantity(100);

        // Act & Assert
        quantity.IsMinimum().Should().BeFalse();
    }

    [Theory, AutoData]
    public void ShouldIncreaseTheValueByOneUnit(int value)
    {
        // Arrange
        var quantity = new Quantity(value);

        // Act
        quantity.Increment();

        // Assert
        quantity.Value.Should().Be(value + 1);
    }
    
    [Theory, AutoData]
    public void ShouldDecrementTheValueByOneUnit(int value)
    {
        // Arrange
        var quantity = new Quantity(value);

        // Act
        quantity.Decrement();

        // Assert
        quantity.Value.Should().Be(value - 1);
    }

    [Theory, AutoData]
    public void ShouldIncreasesValueByGivenQuantity(int value, int incrementValue)
    {
        // Arrange
        var quantity = new Quantity(value);

        // Act
        quantity.Increment(new Quantity(incrementValue));

        // Assert
        quantity.Value.Should().Be(incrementValue + value);
    }
    
    [Theory, AutoData]
    public void ShouldDecrementValueByGivenQuantity(int value, int decrementValue)
    {
        // Arrange
        var quantity = new Quantity(value);

        // Act
        quantity.Decrement(new Quantity(decrementValue));

        // Assert
        quantity.Value.Should().Be(value - decrementValue);
    }
}