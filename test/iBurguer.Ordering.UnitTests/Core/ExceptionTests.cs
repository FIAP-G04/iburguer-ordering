using FluentAssertions;
using iBurguer.Ordering.Core;

namespace iBurguer.Ordering.UnitTests.Core
{
    public class ExceptionTests
    {
        [Fact]
        public void CannotToStartOrderException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.CannotToStartOrderException();

            // Assert
            exception.Message.Should().Be("Only orders in the 'Confirmed' state can initiate preparation.");
        }

        [Fact]
        public void CannotToConfirmOrderException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.CannotToConfirmOrderException();

            // Assert
            exception.Message.Should().Be("Only orders in the 'WaitingForPayment' state can be confirmed.");
        }

        [Fact]
        public void CannotToCompleteOrderException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.CannotToCompleteOrderException();

            // Assert
            exception.Message.Should().Be("Only orders in the 'In Progress' state can be completed for delivery.");
        }

        [Fact]
        public void CannotToDeliverOrderException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.CannotToDeliverOrderException();

            // Assert
            exception.Message.Should().Be("Only orders in the 'ReadyForPickup' state can be released for delivery.");
        }

        [Fact]
        public void CannotToCancelOrderException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.CannotToCancelOrderException();

            // Assert
            exception.Message.Should().Be("Only orders in the 'WaitingForPayment' or 'Confirmed' states can be canceled.");
        }

        [Fact]
        public void ThePickupCodeCannotBeEmptyOrNullException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.ThePickupCodeCannotBeEmptyOrNullException();

            // Assert
            exception.Message.Should().Be("The pickup code cannot be null or empty.");
        }

        [Fact]
        public void InvalidOrderNumberException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.InvalidOrderNumberException();

            // Assert
            exception.Message.Should().Be("A value greater than zero must be provided for the order number.");
        }

        [Fact]
        public void InvalidPriceException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.InvalidPriceException();

            // Assert
            exception.Message.Should().Be("The price cannot have a value equal to zero or negative");
        }

        [Fact]
        public void InvalidQuantityException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.InvalidQuantityException();

            // Assert
            exception.Message.Should().Be("A value greater than zero must be provided for the quantity field.");
        }

        [Fact]
        public void OrderNotFoundException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.OrderNotFoundException();

            // Assert
            exception.Message.Should().Be("No order was found with the specified ID");
        }

        [Fact]
        public void OrderTrackingNotFoundException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.OrderTrackingNotFoundException();

            // Assert
            exception.Message.Should().Be("No order tracking was found the specified ID");
        }

        [Fact]
        public void ProductNameCannotBeNullOrEmptyException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.ProductNameCannotBeNullOrEmptyException();

            // Assert
            exception.Message.Should().Be("Product name cannot be null or empty");
        }

        [Fact]
        public void LeastOneOrderItemException_ShouldHaveCorrectMessage()
        {
            // Arrange & Act
            var exception = new Exceptions.LeastOneOrderItemException();

            // Assert
            exception.Message.Should().Be("The order must have at least one item.");
        }
    }
}
