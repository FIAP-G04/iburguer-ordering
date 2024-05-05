using iBurguer.Orders.Core.Domain;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.UseCases.CompleteOrder
{
    public interface ICompleteOrderUseCase
    {
        Task<OrderStatusResponse> CompleteOrder(Guid orderId, CancellationToken cancellation);
    }
    public class CompleteOrderUseCase : ICompleteOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public CompleteOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<OrderStatusResponse> CompleteOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            OrderNotFound.ThrowIfNull(order);

            order.Complete();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
