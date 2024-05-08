using iBurguer.Ordering.Core.Abstractions;

namespace iBurguer.Ordering.Core.Domain;

public record OrderStatusUpdatedDomainEvent(Guid OrderId, Guid Customer,
        OrderTracking Status) : IDomainEvent;