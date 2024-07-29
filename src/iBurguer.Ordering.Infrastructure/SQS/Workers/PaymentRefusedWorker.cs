using Amazon.SQS.Model;
using iBurguer.Ordering.Infrastructure.SQS.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace iBurguer.Ordering.Infrastructure.SQS.Workers
{
    public class PaymentRefusedWorker : SQSWorker
    {
        public PaymentRefusedWorker(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) { }

        protected override string GetQueue(SQSConfiguration config)
            => config.PaymentRefusedQueue;

        protected override async Task Handle(Message msg, CancellationToken cancellationToken)
        {
            var message = JsonConvert.DeserializeObject<PaymentRefusedDomainEvent>(msg.Body);

            await _cancelOrderUseCase.CancelOrder(message.OrderId, cancellationToken);
        }
    }
}
