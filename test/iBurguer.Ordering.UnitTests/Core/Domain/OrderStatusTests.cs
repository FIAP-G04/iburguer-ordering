using FluentAssertions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Core.Domain;

public class OrderStatusTests
{
    [Fact]
    public void ShouldCreateAValidInstanceOfWaitingForPaymentStatus()
    {
        // Arrange & Act
        var status = OrderStatus.WaitingForPayment;

        // Assert
        status.Value.Should().Be(1);
        status.Name.Should().Be("WaitingForPayment");
    }

    [Fact]
    public void ShouldCreateAValidInstanceOfConfirmedStatus()
    {
        // Arrange & Act
        var status = OrderStatus.Confirmed;

        // Assert
        status.Value.Should().Be(2);
        status.Name.Should().Be("Confirmed");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfInProgressStatus()
    {
        // Arrange & Act
        var status = OrderStatus.InProgress;

        // Assert
        status.Value.Should().Be(3);
        status.Name.Should().Be("InProgress");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfReadyForPickupStatus()
    {
        // Arrange & Act
        var status = OrderStatus.ReadyForPickup;

        // Assert
        status.Value.Should().Be(4);
        status.Name.Should().Be("ReadyForPickup");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfPickedUpStatus()
    {
        // Arrange & Act
        var status = OrderStatus.PickedUp;

        // Assert
        status.Value.Should().Be(5);
        status.Name.Should().Be("PickedUp");
    }
    
    [Fact]
    public void ShouldCreateAValidInstanceOfCanceledStatus()
    {
        // Arrange & Act
        var status = OrderStatus.Canceled;

        // Assert
        status.Value.Should().Be(6);
        status.Name.Should().Be("Canceled");
    }
}