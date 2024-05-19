using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.Domain;

public sealed record Quantity
{
    private int _value = 1;
    
    private Quantity() {}

    public Quantity(int quantity) => Value = quantity;

    public int Value
    {
        get => _value;
        private set
        {
            InvalidQuantityException.ThrowIf(value < 1);

            _value = value;
        }
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;

    public static implicit operator Quantity(int value) => new(value);

    public override string ToString() => Value.ToString();

    public bool IsMinimum() => Value == 1;

    public void Increment() => Value++;

    public void Increment(Quantity quantity) => Value = Value + quantity.Value;

    public void Decrement() => Value--;

    public void Decrement(Quantity quantity) => Value = Value - quantity.Value;
}