using System.Text.Json.Serialization;
using iBurguer.Ordering.Core.Abstractions;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.Domain;

public class Order : Entity<Guid>, IAggregateRoot
{
    private readonly List<OrderTracking> _trackings = new();
    private readonly IList<OrderItem> _items;
    
    public OrderNumber Number { get; init; }
    public OrderType Type { get; init; }
    public PickupCode PickupCode { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public Guid? BuyerId { get; init; }
    public DateTime CreatedAt { get; init; }

    private Order()
    {
        _items = new List<OrderItem>();
    }

    public Order(OrderNumber number, OrderType type, PaymentMethod paymentMethod, Guid? buyerId, IList<OrderItem> items)
    {
        InvalidOrderNumberException.ThrowIfNull(number);
        LeastOneOrderItemException.ThrowIf(!items.Any());
        
        Number = number;
        Type = type;
        PickupCode = PickupCode.Generate();
        PaymentMethod = paymentMethod;
        BuyerId = buyerId;
        CreatedAt = DateTime.Now;

        foreach (var item in items)
        {
            item.SetOrder(this);
        }
        
        _items = items;
        _trackings.Add(new OrderTracking(OrderStatus.WaitingForPayment, this));
    }

    public IReadOnlyCollection<OrderTracking> Trackings => _trackings.AsReadOnly();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money Total => _items.Sum(i => i.Subtotal);

    public bool IsPaid => _trackings.Any(t => t.OrderStatus != OrderStatus.WaitingForPayment && t.OrderStatus != OrderStatus.Canceled);

    public OrderStatus CurrentStatus => _trackings.OrderByDescending(s => s.When).First().OrderStatus;
    
    [JsonIgnore]
    public OrderTracking CurrentTracking => _trackings.OrderByDescending(s => s.When).First();

    public void Confirm()
    {
        CannotToConfirmOrderException.ThrowIf(CurrentStatus != OrderStatus.WaitingForPayment);

        _trackings.Add(new OrderTracking(OrderStatus.Confirmed, this));
    }

    public void Start()
    {
        CannotToStartOrderException.ThrowIf(CurrentStatus != OrderStatus.Confirmed);

        _trackings.Add(new OrderTracking(OrderStatus.InProgress, this));
    }

    public void Complete()
    {
        CannotToCompleteOrderException.ThrowIf(CurrentStatus != OrderStatus.InProgress);

        _trackings.Add(new OrderTracking(OrderStatus.ReadyForPickup, this));
    }

    public void Deliver()
    {
        CannotToDeliverOrderException.ThrowIf(CurrentStatus != OrderStatus.ReadyForPickup);

        _trackings.Add(new OrderTracking(OrderStatus.PickedUp, this));
    }

    public void Cancel()
    {
        CannotToCancelOrderException.ThrowIf(CurrentStatus != OrderStatus.WaitingForPayment && CurrentStatus != OrderStatus.Confirmed);

        _trackings.Add(new OrderTracking(OrderStatus.Canceled, this));
    }
}