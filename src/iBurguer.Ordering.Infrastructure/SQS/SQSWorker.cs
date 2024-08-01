using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using System.Diagnostics.CodeAnalysis;
using iBurguer.Ordering.Core.Abstractions;

namespace iBurguer.Ordering.Infrastructure.SQS
{
    [ExcludeFromCodeCoverage]
    public abstract class SQSWorker : BackgroundService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IServiceScopeFactory _scopeFactory;
        protected ISQSService _sqsService;
        protected IRegisterOrderUseCase _registerOrderUseCase;
        protected IConfirmOrderUseCase _confirmOrderUseCase;
        protected ICancelOrderUseCase _cancelOrderUseCase;

        public SQSWorker(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected abstract Task Handle(Message message, CancellationToken cancellationToken);

        protected abstract string GetQueue(SQSConfiguration config);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configuration = _configuration.GetRequiredSection("MassTransit").Get<SQSConfiguration>();

            using IServiceScope serviceScope = _scopeFactory.CreateScope();

            _registerOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<IRegisterOrderUseCase>();
            _confirmOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<IConfirmOrderUseCase>();
            _cancelOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<ICancelOrderUseCase>();
            _sqsService = serviceScope.ServiceProvider.GetRequiredService<ISQSService>();

            var queueName = GetQueue(configuration);
            var queueUrl = await _sqsService.GetQueueUrl(queueName);

            await Start(queueUrl, stoppingToken);

        }

        private async Task Start(string queueUrl, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Starting polling queue");

            while (!cancellationToken.IsCancellationRequested)
            {
                var messages = await _sqsService.ReceiveMessageAsync(queueUrl);

                if (messages.Any())
                {
                    Console.WriteLine($"{messages.Count()} messages received");

                    foreach (var msg in messages)
                    {
                        await Handle(msg, cancellationToken);
                        await _sqsService.DeleteMessageAsync(queueUrl, msg.ReceiptHandle);
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
        }
    }
}
