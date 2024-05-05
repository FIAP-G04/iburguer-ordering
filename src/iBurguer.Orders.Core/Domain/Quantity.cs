using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.Domain;

public sealed record Quantity
{
    private ushort _value = 1;
    
    private Quantity() {}

    public Quantity(ushort quantity) => Value = quantity;

    public ushort Value
    {
        get => _value;
        private set
        {
            InvalidQuantity.ThrowIf(value < 1);

            _value = value;
        }
    }

    public static implicit operator ushort(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(ushort value) => new(value);

    public override string ToString() => Value.ToString();

    public bool IsMinimum() => Value == 1;

    public void Increment() => Value++;

    public void Increment(Quantity quantity) => Value = (ushort)(Value + quantity.Value);

    public void Decrement() => Value--;

    public void Decrement(Quantity quantity) => Value = (ushort)(Value - quantity.Value);
}