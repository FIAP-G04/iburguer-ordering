using iBurguer.Orders.Core.Abstractions;

namespace iBurguer.Orders.Core.Domain;

public class OrderTracking
{
    public Guid TrackingId { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public DateTime When { get; init; }
    public Order Order { get; init; }
    
    private OrderTracking() {}

    public OrderTracking(OrderStatus orderStatus, Order order)
    {
        TrackingId = Guid.NewGuid();
        OrderStatus = orderStatus;
        When = DateTime.Now;
        Order = order;
    }
}