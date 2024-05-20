using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.UseCases.GetPagedOrders;

[ExcludeFromCodeCoverage]
public record OrderItemSummaryResponse()
{
    public Guid OrderItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Subtotal { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductType { get; set; }
    
    public static OrderItemSummaryResponse Convert(OrderItem item)
    {
        return new OrderItemSummaryResponse
        {
            OrderItemId = item.Id,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            Subtotal = item.Subtotal,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            ProductType = item.ProductType.Name
        };
    }
}