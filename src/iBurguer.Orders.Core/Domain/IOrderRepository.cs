namespace iBurguer.Orders.Core.Domain;

public interface IOrderRepository
{
    Task Save(Order order, CancellationToken cancellation);

    Task Update(Order order, CancellationToken cancellation);

    Task<Order?> GetById(Guid orderId, CancellationToken cancellation);

    Task<int> GenerateOrderNumber(CancellationToken cancellation);
}