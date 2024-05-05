using iBurguer.Orders.Core.Abstractions;

namespace iBurguer.Orders.Core.Domain;

public record OrderStatusUpdatedDomainEvent(Guid OrderId, Guid Customer,
        OrderTracking Status) : IDomainEvent;