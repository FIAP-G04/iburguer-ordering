using static iBurguer.Orders.Core.Exceptions;
using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases.ConfirmOrder
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

            OrderNotFound.ThrowIfNull(order);

            order.Confirm();

            await _repository.Update(order, cancellation);
            
            return OrderStatusResponse.Convert(order);
        }
    }
}
