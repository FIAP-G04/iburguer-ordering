using System.Reflection;
using Ardalis.SmartEnum;
using static iBurguer.Orders.Core.Exceptions;

namespace iBurguer.Orders.Core.Domain;

public sealed class PaymentMethod : SmartEnum<PaymentMethod>
{
    public static readonly PaymentMethod QrCode = new PaymentMethod("QRCode", 1);

    private PaymentMethod(string name, int value) : base(name, value) { }
}