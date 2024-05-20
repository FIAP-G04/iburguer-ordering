using AutoFixture.Kernel;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.UnitTests.Util;

public class OrderBuilder
{
    public OrderNumber Number { get; private set; }
    public OrderType Type { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    
    public OrderBuilder WithNumber(int value)
    {
        Number = new OrderNumber(value);

        return this;
    }
    
    public OrderBuilder WithType(string value)
    {
        Type = OrderType.FromName(value);

        return this;
    }
    
    public OrderBuilder WithPaymentMethod(string value)
    {
        PaymentMethod = PaymentMethod.FromName(value);

        return this;
    }
    
    public OrderBuilder WithOrderItems()
    {
        var item = OrderItem.Create(Guid.NewGuid(), "Batata", ProductType.SideDish, 20, 1);
        
        Items.Add(item);

        return this;
    }
    
    public Order Build()
    {
        return new Order(Number, Type, PaymentMethod, Guid.NewGuid(), Items);
    }
}