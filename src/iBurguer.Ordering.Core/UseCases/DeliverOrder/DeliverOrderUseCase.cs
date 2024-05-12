using iBurguer.Ordering.Core.Domain;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.UseCases.DeliverOrder
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

            Exceptions.OrderNotFoundException.ThrowIfNull(order);

            order.Deliver();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
