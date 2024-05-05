using static iBurguer.Orders.Core.Exceptions;
using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases.DeliverOrder
{
    public interface IDeliverOrderUseCase
    {
        Task<OrderStatusResponse> DeliverOrder(Guid orderId, CancellationToken cancellation);
    }

    public class DeliverOrderUseCase : IDeliverOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public DeliverOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<OrderStatusResponse> DeliverOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            OrderNotFound.ThrowIfNull(order);

            order.Deliver();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
