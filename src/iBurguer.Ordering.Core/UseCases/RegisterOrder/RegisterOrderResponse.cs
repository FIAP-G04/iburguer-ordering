using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases.RegisterOrder;

public record RegisterOrderResponse
{
    public Guid OrderId { get; set; }
    public int OrderNumber { get; set; }
    public string PickupCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    public static RegisterOrderResponse Convert(Order order)
    {
        return new RegisterOrderResponse
        {
            OrderId = order.Id,
            OrderNumber = order.Number,
            PickupCode = order.PickupCode,
            CreatedAt = order.CreatedAt
        };
    }
}