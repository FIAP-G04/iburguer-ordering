using iBurguer.Orders.Core.Domain;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.UseCases.CancelOrder
{
    public interface ICancelOrderUseCase
    {
        Task<OrderStatusResponse> CancelOrder(Guid orderId, CancellationToken cancellation);
    }

    public class CancelOrderUseCase : ICancelOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public CancelOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<OrderStatusResponse> CancelOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            OrderNotFound.ThrowIfNull(order);

            order.Cancel();

            await _repository.Update(order, cancellation);

            return OrderStatusResponse.Convert(order);
        }
    }
}
