using Newtonsoft.Json;

namespace iBurguer.Ordering.Infrastructure.SQS.Events
{
    public record PaymentConfirmedDomainEvent
    {
        [JsonProperty("orderId")]
        public Guid OrderId;
    }
}
