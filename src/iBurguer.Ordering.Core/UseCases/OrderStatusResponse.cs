using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases;

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
            OrderStatus = tracking.OrderStatus.ToString(),
            UpdatedAt = tracking.When
        };
    }
}