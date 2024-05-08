using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases.RegisterOrder
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
            
            var order = new Order(
                number, 
                OrderType.FromName(request.OrderType), 
                PaymentMethod.FromName(request.PaymentMethod), 
                request.BuyerId, 
                request.Items.Select(i => CreateItem(i)).ToList());

            await _repository.Save(order, cancellation);

            return RegisterOrderResponse.Convert(order);
        }

        private OrderItem CreateItem(OrderItemRequest request)
        {
            return OrderItem.Create(
                request.ProductId,
                request.ProductName,
                ProductType.FromName(request.ProductType),
                request.UnitPrice,
                request.Quantity);
        }
    }
}
