using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases.RegisterOrder;

public record RegisterOrderResponse
{
    public Guid OrderId { get; set; }
    public int OrderNumber { get; set; }
    public string PickupCode { get; set; }
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