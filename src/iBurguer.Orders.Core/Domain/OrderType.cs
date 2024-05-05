using System.Reflection;
using iBurguer.Orders.Core.Abstractions;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.Domain;

public sealed class OrderType : KeyValue<OrderType>
{
    public static readonly OrderType EatIn = new(1, "EatIn");
    public static readonly OrderType TakeAway = new(2, "TakeAway");

    private OrderType(int value, string name) : base(value, name) { }
    
    public static implicit operator OrderType(int value) => Find(type => type.Id == value);
    
    public new void Throw(OrderType value)
    {
        InvalidOrderType.ThrowIfNull(value);
    }
}
