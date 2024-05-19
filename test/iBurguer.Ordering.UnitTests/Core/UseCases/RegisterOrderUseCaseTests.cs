using AutoFixture.Xunit2;
using FluentAssertions;
using static iBurguer.Ordering.Core.Exceptions;
using iBurguer.Ordering.Core.Domain;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using iBurguer.Ordering.UnitTests.Util;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace iBurguer.Ordering.UnitTests.Core.UseCases;

public class RegisterOrderUseCaseTests : BaseTests
{
    private readonly IOrderRepository _repository;
    private readonly IRegisterOrderUseCase _sut;

    public RegisterOrderUseCaseTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _sut = new RegisterOrderUseCase(_repository);
    }

    [Fact]
    public async Task ShouldRegisterOrder()
    {
        // Arrange
        var request = new RegisterOrderRequest
        {
            BuyerId = Guid.NewGuid(),
            PaymentMethod = "QRCode",
            OrderType = "TakeAway",
            Items = new List<OrderItemRequest>()
            {
                new()
                {
                    Quantity = 1,
                    ProductId = Guid.NewGuid(),
                    ProductName = "Batata",
                    ProductType = "SideDish",
                    UnitPrice = 20
                }
            }
        };
        
        _repository.GenerateOrderNumber(Arg.Any<CancellationToken>()).Returns(100);

        // Act
        var result = await _sut.RegisterOrder(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderNumber.Should().Be(100);
        result.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }
}