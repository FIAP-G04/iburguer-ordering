using Ardalis.SmartEnum;

namespace iBurguer.Ordering.Core.Domain;

public class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus WaitingForPayment = new("WaitForPayment", 1);
    public static readonly OrderStatus Confirmed = new("Confirmed", 2);
    public static readonly OrderStatus InProgress = new("InProgress", 3);
    public static readonly OrderStatus ReadyForPickup = new("ReadyForPickup", 4);
    public static readonly OrderStatus PickedUp = new("PickedUp", 5);
    public static readonly OrderStatus Canceled = new("Canceled", 6);

    private OrderStatus(string name, int value) : base(name, value) { }
    
}