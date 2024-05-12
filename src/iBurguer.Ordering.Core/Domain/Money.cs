using System.Globalization;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.Domain;

public sealed record Money
{
    public decimal Amount { get; } = 0;

    public Money(decimal amount)
    {
        InvalidPriceException.ThrowIf(amount < 0);

        Amount = amount;
    }

    public override string ToString() => Amount.ToString(CultureInfo.InvariantCulture);

    public static implicit operator decimal(Money money) => money.Amount;

    public static implicit operator Money(decimal value) => new(value);
}