using Amazon.SQS.Model;
using iBurguer.Ordering.Infrastructure.SQS.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Ordering.Infrastructure.SQS.Workers
{
    [ExcludeFromCodeCoverage]
    public class PaymentConfirmedWorker : SQSWorker
    {
        public PaymentConfirmedWorker(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) { }

        protected override string GetQueue(SQSConfiguration config)
            => config.PaymentApprovedQueue;

        protected override async Task Handle(Message msg, CancellationToken cancellationToken)
        {
            var message = JsonConvert.DeserializeObject<PaymentConfirmedDomainEvent>(msg.Body);

            await _confirmOrderUseCase.ConfirmOrder(message.OrderId, cancellationToken);
        }
    }
}
