using iBurguer.Ordering.Core.Abstractions;

namespace iBurguer.Ordering.Core.Domain
{
    public class OrderRegisteredDomainEvent : IDomainEvent
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
