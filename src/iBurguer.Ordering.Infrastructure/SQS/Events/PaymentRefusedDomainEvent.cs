using Newtonsoft.Json;

namespace iBurguer.Ordering.Infrastructure.SQS.Events
{
    public record PaymentRefusedDomainEvent
    {
        [JsonProperty("orderId")]
        public Guid OrderId;
    }
}
