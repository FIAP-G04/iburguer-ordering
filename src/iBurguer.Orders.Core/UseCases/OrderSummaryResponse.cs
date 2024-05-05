using iBurguer.Orders.Core.Domain;

namespace iBurguer.Orders.Core.UseCases;

public record OrderSummaryResponse()
{
    public Guid OrderId { get; set; }

    public int OrderNumber { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public Guid ShoppingCartId { get; set; }

    public Guid? CustomerId { get; set; }

    public IList<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();

    public decimal Total { get; set; }
}

public record OrderItemResponse()
{
    public Guid OrderItemId { get; set; }

    public ushort Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Subtotal { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

    //public Category Category { get; set; }
}