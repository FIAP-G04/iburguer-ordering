using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.Domain;

public record OrderNumber
{
    public int Value { get; private set; }
    
    private OrderNumber(){}
    
    public OrderNumber(int value)
    {
        InvalidOrderNumberException.ThrowIf(value <= 0);
        
        Value = value;
    }

    public static implicit operator int(OrderNumber orderNumber) => orderNumber.Value;

    public static implicit operator OrderNumber(int value) => new(value);
}