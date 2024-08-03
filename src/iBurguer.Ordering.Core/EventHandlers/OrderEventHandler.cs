using iBurguer.Ordering.Core.Abstractions;
using iBurguer.Ordering.Core.Domain;

namespace iBurguer.Ordering.Core.EventHandlers
{
    public class OrderEventHandler : IEventHandler<OrderRegisteredDomainEvent>
    {
        private readonly ISQSService _service;

        public OrderEventHandler(Abstractions.ISQSService service)
        {
            _service = service;
        }

        private readonly string _orderRegisteredQueue = "OrderRegistered";

        public async Task Handle(OrderRegisteredDomainEvent evt, CancellationToken cancellation)
        {
            await _service.SendMessage(evt, _orderRegisteredQueue, cancellation);
        }
    }
}
