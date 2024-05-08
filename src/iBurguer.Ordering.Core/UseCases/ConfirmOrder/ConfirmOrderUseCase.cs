using iBurguer.Ordering.Core.Domain;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.UseCases.ConfirmOrder
{
    public interface IConfirmOrderUseCase
    {
        Task<OrderStatusResponse> ConfirmOrder(Guid orderId, CancellationToken cancellation);
    }

    public class ConfirmOrderUseCase : IConfirmOrderUseCase
    {
        private readonly IOrderRepository _repository;

        public ConfirmOrderUseCase(IOrderRepository repository) => _repository = repository;

        public async Task<OrderStatusResponse> ConfirmOrder(Guid orderId, CancellationToken cancellation)
        {
            var order = await _repository.GetById(orderId, cancellation);

            Exceptions.OrderNotFound.ThrowIfNull(order);

            order.Confirm();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
