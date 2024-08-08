using iBurguer.Ordering.Core.Abstractions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.EventHandlers;
using Moq;

namespace iBurguer.Ordering.Core.Tests.EventHandlers
{
    public class OrderEventHandlerTests
    {
        private readonly Mock<ISQSService> _mockSQSService;
        private readonly OrderEventHandler _orderEventHandler;

        public OrderEventHandlerTests()
        {
            _mockSQSService = new Mock<ISQSService>();
            _orderEventHandler = new OrderEventHandler(_mockSQSService.Object);
        }

        [Fact]
        public async Task Handle_Should_Call_SendMessage_With_Correct_Parameters()
        {
            // Arrange
            var evt = new OrderRegisteredDomainEvent();
            var cancellationToken = new CancellationToken();

            // Act
            await _orderEventHandler.Handle(evt, cancellationToken);

            // Assert
            _mockSQSService.Verify(
                x => x.SendMessage(evt, "OrderRegistered", cancellationToken),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_If_Service_Throws_Exception()
        {
            // Arrange
            var evt = new OrderRegisteredDomainEvent();
            var cancellationToken = new CancellationToken();
            _mockSQSService.Setup(x => x.SendMessage(It.IsAny<OrderRegisteredDomainEvent>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new System.Exception("Service exception"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _orderEventHandler.Handle(evt, cancellationToken));
        }
    }
}
