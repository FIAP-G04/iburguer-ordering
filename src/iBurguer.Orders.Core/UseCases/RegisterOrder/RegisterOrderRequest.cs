using FluentValidation;

namespace iBurguer.Orders.Core.UseCases.RegisterOrder;

public record RegisterOrderRequest
{
    public int OrderType { get; set; }
    public int PaymentMethod { get; init; }
    public Guid? BuyerId { get; init; }
    public IList<OrderItemRequest> Items { get; set; }
}

public record OrderItemRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductType { get; set; }
    public decimal UnitPrice { get; set; }
    public ushort Quantity { get; set; }
}