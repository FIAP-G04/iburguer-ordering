using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases.GetPagedOrders;

public record OrderSummaryResponse()
{
    public Guid OrderId { get; set; }

    public int OrderNumber { get; set; }

    public string OrderType { get; set; }
    
    public string OrderStatus { get; set; }

    public string PaymentMethod { get; set; }
    
    public Guid? BuyerId { get; set; }

    public IList<OrderItemSummaryResponse> Items { get; set; } = new List<OrderItemSummaryResponse>();

    public decimal Total { get; set; }
    
    public static OrderSummaryResponse Convert(Order order)
    {
        return new OrderSummaryResponse
        {
            OrderId = order.Id,
            OrderType = order.Type.Name,
            OrderNumber = order.Number,
            OrderStatus = order.CurrentStatus.Name,
            PaymentMethod = order.PaymentMethod.Name,
            BuyerId = order.BuyerId,
            Total = order.Total,
            Items = order.Items.Select(item => OrderItemSummaryResponse.Convert(item)).ToList()
        };
    }
}

