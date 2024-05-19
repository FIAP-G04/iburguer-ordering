using FluentAssertions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class OrderTypeTests
{
    [Fact]
    public void ShouldCreateAValidInstanceOfEatInOrderType()
    {
        // Arrange & Act
        var status = OrderType.EatIn;

        // Assert
        status.Value.Should().Be(1);
        status.Name.Should().Be("EatIn");
    }

    [Fact]
    public void ShouldCreateAValidInstanceOfTakeAwayOrderType()
    {
        // Arrange & Act
        var status = OrderType.TakeAway;

        // Assert
        status.Value.Should().Be(2);
        status.Name.Should().Be("TakeAway");
    }
}