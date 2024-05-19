using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class MoneyTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(1000)]
    public void ShouldReturnValidValue(decimal value)
    {
        // Arrange & Act
        var money = new Money(value);

        // Assert
        money.Amount.Should().Be(value);
    }

    [Fact]
    public void ShouldThrowExceptionForNegativeValues()
    {
        // Arrange, Act & Assert
        Assert.Throws<InvalidPriceException>(() => new Money(-1));
    }

    [Fact]
    public void ShouldReturnCorrectTextualRepresentation()
    {
        // Arrange
        var money = new Money(50);

        // Act
        var result = money.ToString();

        // Assert
        result.Should().Be("50");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(1000)]
    public void ShouldPerformImplicitConversionFromDecimalToMoneyCorrectly(decimal value)
    {
        // Arrange
        Money money = new Money(value);

        // Act
        decimal result = money;

        // Assert
        result.Should().Be(value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(1000)]
    public void ShouldPerformImplicitConversionFromMoneyToDecimalCorrectly(decimal value)
    {
        // Arrange
        Money money = value;

        // Act
        decimal result = money.Amount;

        // Assert
        result.Should().Be(value);
    }
}