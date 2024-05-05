using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases;

public record OrderStatusResponse
{
    public Guid OrderId { get; set; }
    public string OrderStatus { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static OrderStatusResponse Convert(Order order)
    {
        var tracking = order.CurrentTracking;
        
        return new OrderStatusResponse
        {
            OrderId = order.Id,
            OrderStatus = Enum.GetName(typeof(OrderStatus), tracking.OrderStatus),
            UpdatedAt = tracking.When
        };
    }
}