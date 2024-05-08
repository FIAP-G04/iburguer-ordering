using FluentValidation;

namespace iBurguer.Ordering.Core.UseCases.RegisterOrder;

public record RegisterOrderRequest
{
    public string OrderType { get; set; }
    public string PaymentMethod { get; init; }
    public Guid? BuyerId { get; init; }
    public IList<OrderItemRequest> Items { get; set; }
}

public record OrderItemRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public decimal UnitPrice { get; set; }
    public ushort Quantity { get; set; }
}