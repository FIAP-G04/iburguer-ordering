using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.CompleteOrder;
using iBurguer.Ordering.UnitTests.Util;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Ordering.UnitTests.Core.UseCases;

public class CompleteOrderUseCaseTests : BaseTests
{
    private readonly IOrderRepository _repository;
    private readonly ICompleteOrderUseCase _sut;

    public CompleteOrderUseCaseTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _sut = new CompleteOrderUseCase(_repository);
    }

    [Fact]
    public async Task ShouldCompleteOrderWithValidOrderId()
    {
        // Arrange
        var order = new OrderBuilder().WithNumber(1)
                                      .WithType("TakeAway")
                                      .WithPaymentMethod("QRCode")
                                      .WithOrderItems()
                                      .Build();
        
        order.Confirm();
        order.Start();
        
        _repository.GetById(order.Id, Arg.Any<CancellationToken>()).Returns(order);

        // Act
        var result = await _sut.CompleteOrder(order.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(order.Id);
        result.OrderStatus.Should().Be("ReadyForPickup");
        result.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }

    [Theory, AutoData]
    public async Task ShouldThrowsOrderNotFoundExceptionWhenCompletingAOrderWithNonExistingOrderId(
        Guid invalidOrderId)
    {
        // Arrange
        var repository = Substitute.For<IOrderRepository>();
        repository.GetById(invalidOrderId, Arg.Any<CancellationToken>()).ReturnsNull();

        // Act
        Func<Task> act = async () => await _sut.CompleteOrder(invalidOrderId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>();
    }
}