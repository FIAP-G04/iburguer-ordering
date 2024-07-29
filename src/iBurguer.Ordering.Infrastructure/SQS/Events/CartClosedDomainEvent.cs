using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Ordering.Infrastructure.SQS.Events
{
    [ExcludeFromCodeCoverage]
    public record CartClosedDomainEvent
    {
        [JsonProperty("orderType")]
        public string OrderType { get; set; }
        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }
        [JsonProperty("buyerId")]
        public Guid? BuyerId { get; set; }
        [JsonProperty("items")]
        public IList<OrderItemRequest> Items { get; set; }
    }

    public record OrderItemRequest
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }
        [JsonProperty("productName")]
        public string ProductName { get; set; }
        [JsonProperty("productType")]
        public string ProductType { get; set; }
        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }
        [JsonProperty("quantity")]
        public ushort Quantity { get; set; }
    }
}
