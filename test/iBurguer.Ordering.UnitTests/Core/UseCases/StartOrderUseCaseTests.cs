using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.StartOrder;
using iBurguer.Ordering.UnitTests.Util;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Ordering.UnitTests.Core.UseCases;

public class StartOrderUseCaseTests : BaseTests
{
    private readonly IOrderRepository _repository;
    private readonly IStartOrderUseCase _sut;

    public StartOrderUseCaseTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _sut = new StartOrderUseCase(_repository);
    }

    [Fact]
    public async Task ShouldStartOrderWithValidOrderId()
    {
        // Arrange
        var order = new OrderBuilder().WithNumber(1)
                                      .WithType("TakeAway")
                                      .WithPaymentMethod("QRCode")
                                      .WithOrderItems()
                                      .Build();
        
        order.Confirm();
        
        _repository.GetById(order.Id, Arg.Any<CancellationToken>()).Returns(order);

        // Act
        var result = await _sut.StartOrder(order.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(order.Id);
        result.OrderStatus.Should().Be("InProgress");
        result.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }

    [Theory, AutoData]
    public async Task ShouldThrowsOrderNotFoundExceptionWhenStartingAOrderWithNonExistingOrderId(
        Guid invalidOrderId)
    {
        // Arrange
        _repository.GetById(invalidOrderId, Arg.Any<CancellationToken>()).ReturnsNull();

        // Act
        Func<Task> act = async () => await _sut.StartOrder(invalidOrderId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>();
    }
}