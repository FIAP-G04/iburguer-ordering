using iBurguer.Ordering.Core.Domain;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.UseCases.StartOrder
{
    public interface IStartOrderUseCase
    {
        Task<OrderStatusResponse> StartOrder(Guid orderId, CancellationToken cancellation);
    }

    public class StartOrderUseCase : IStartOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public StartOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<OrderStatusResponse> StartOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            Exceptions.OrderNotFound.ThrowIfNull(order);

            order.Start();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
