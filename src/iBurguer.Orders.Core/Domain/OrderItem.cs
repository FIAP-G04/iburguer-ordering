using iBurguer.Orders.Core.Abstractions;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.Domain;

public class OrderItem : Entity<Guid>
{
    public Guid ProductId { get; private set; }
    
    public Order Order { get; private set; }
    
    private string _productName;
    public string ProductName
    {
        get { return _productName; }
        private set 
        {
            ProductNameCannotBeNullOrEmpty.ThrowIfEmpty(value);
            
            _productName = value;
        }
    }
    
    public ProductType ProductType { get; private set; }
    
    public Money UnitPrice { get; private set; }
    
    public Quantity Quantity { get; private set; }
    
    private OrderItem() {}

    public static OrderItem Create(Guid productId, string productName, ProductType productType, Money price, Quantity quantity)
    {
        return new OrderItem
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductName = productName,
            ProductType = productType,
            UnitPrice = price,
            Quantity = quantity
        };
    }

    public void SetOrder(Order order)
    {
        this.Order = order;
    }

    public Money Subtotal => Quantity * UnitPrice;
}