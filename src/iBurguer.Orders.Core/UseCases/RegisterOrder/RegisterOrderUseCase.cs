using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases.RegisterOrder
{
    public interface IRegisterOrderUseCase
    {
        Task<RegisterOrderResponse> RegisterOrder(RegisterOrderRequest request, CancellationToken cancellation);
    }

    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public RegisterOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<RegisterOrderResponse> RegisterOrder(RegisterOrderRequest request, CancellationToken cancellation)
        {
            var number = await _repository.GenerateOrderNumber(cancellation);

            var items = request.Items.Select(i => OrderItem.Create(i.ProductId, i.ProductName,
                (ProductType)i.ProductType,
                i.UnitPrice, i.Quantity)).ToList();
            
            var order = new Order(number, request.OrderType, (PaymentMethod)request.PaymentMethod, request.BuyerId, items);

            await _repository.Save(order, cancellation);

            return RegisterOrderResponse.Convert(order);
        }
    }
}
