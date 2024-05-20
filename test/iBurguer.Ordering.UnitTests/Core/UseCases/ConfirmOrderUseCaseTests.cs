using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.UnitTests.Util;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Ordering.UnitTests.Core.UseCases;

public class ConfirmOrderUseCaseTests : BaseTests
{
    private readonly IOrderRepository _repository;
    private readonly IConfirmOrderUseCase _sut;

    public ConfirmOrderUseCaseTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _sut = new ConfirmOrderUseCase(_repository);
    }

    [Fact]
    public async Task ShouldConfirmOrderWithValidOrderId()
    {
        // Arrange
        var order = new OrderBuilder().WithNumber(1)
                                      .WithType("TakeAway")
                                      .WithPaymentMethod("QRCode")
                                      .WithOrderItems()
                                      .Build();
        
        _repository.GetById(order.Id, Arg.Any<CancellationToken>()).Returns(order);

        // Act
        var result = await _sut.ConfirmOrder(order.Id, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(order.Id);
        result.OrderStatus.Should().Be("Confirmed");
        result.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }

    [Theory, AutoData]
    public async Task ShouldThrowsOrderNotFoundExceptionWhenConfirmingAOrderWithNonExistingOrderId(
        Guid invalidOrderId)
    {
        // Arrange
        _repository.GetById(invalidOrderId, Arg.Any<CancellationToken>()).ReturnsNull();

        // Act
        Func<Task> act = async () => await _sut.ConfirmOrder(invalidOrderId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<OrderNotFoundException>();
    }
}