using Ardalis.SmartEnum;

namespace iBurguer.Ordering.Core.Domain;

public sealed class OrderType : SmartEnum<OrderType>
{
    public static readonly OrderType EatIn = new("EatIn", 1);
    public static readonly OrderType TakeAway = new("TakeAway", 1);

    private OrderType(string name, int value) : base(name, value) { }
}
