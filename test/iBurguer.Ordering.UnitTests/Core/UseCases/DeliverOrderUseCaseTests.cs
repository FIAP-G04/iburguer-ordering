using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.DeliverOrder;
using iBurguer.Ordering.Core.UseCases.StartOrder;
using iBurguer.Ordering.UnitTests.Util;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Ordering.UnitTests.Core.UseCases;

public class DeliverOrderUseCaseTests : BaseTests
{
    private readonly IOrderRepository _repository;
    private readonly IDeliverOrderUseCase _sut;

    public DeliverOrderUseCaseTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _sut = new DeliverOrderUseCase(_repository);
    }

    [Fact]
    public async Task ShouldDeliverOrderWithValidOrderId()
    {
        // Arrange
        var order = new OrderBuilder().WithNumber(1)
                                      .WithType("TakeAway")
                                      .WithPaymentMethod("QRCode")
                                      .WithOrderItems()
                                      .Build();
        
        order.Confirm();
        order.Start();
        order.Complete();
        
        _repository.GetById(order.Id, Arg.Any<CancellationToken>()).Returns(order);

        // Act
        var result = await _sut.DeliverOrder(order.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(order.Id);
        result.OrderStatus.Should().Be("PickedUp");
        result.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }

    [Theory, AutoData]
    public async Task ShouldThrowsOrderNotFoundExceptionWhenDeliveringAOrderWithNonExistingOrderId(
        Guid invalidOrderId)
    {
        // Arrange
        _repository.GetById(invalidOrderId, Arg.Any<CancellationToken>()).ReturnsNull();

        // Act
        Func<Task> act = async () => await _sut.DeliverOrder(invalidOrderId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>();
    }
}