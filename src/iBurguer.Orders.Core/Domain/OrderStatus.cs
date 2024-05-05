namespace iBurguer.Orders.Core.Domain;

public enum OrderStatus : ushort
{
    WaitingForPayment = 1,
    Confirmed = 2,
    InProgress = 3,
    ReadyForPickup = 4,
    PickedUp = 5,
    Canceled = 6
}