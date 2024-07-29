using Amazon.SQS.Model;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using iBurguer.Ordering.Infrastructure.SQS.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using OrderItemRequest = iBurguer.Ordering.Core.UseCases.RegisterOrder.OrderItemRequest;

namespace iBurguer.Ordering.Infrastructure.SQS.Workers
{
    [ExcludeFromCodeCoverage]
    public class CartClosedWorker : SQSWorker
    {
        public CartClosedWorker(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) { }

        protected override string GetQueue(SQSConfiguration config)
            => config.CartClosedQueue;

        protected override async Task Handle(Message msg, CancellationToken cancellationToken)
        {
            var message = JsonConvert.DeserializeObject<CartClosedDomainEvent>(msg.Body);

            var request = new RegisterOrderRequest()
            {
                OrderType = message.OrderType,
                PaymentMethod = message.PaymentMethod,
                BuyerId = message.BuyerId,
                Items = message.Items.Select(x => new OrderItemRequest()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductType = x.ProductType,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                }).ToList()
            };

            await _registerOrderUseCase.RegisterOrder(request, cancellationToken);
        }
    }
}
