using Amazon.Runtime;
using Amazon.SQS.Model;
using Amazon.SQS;
using Microsoft.Extensions.Hosting;
using Amazon;
using iBurguer.Ordering.Core.UseCases.RegisterOrder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using iBurguer.Ordering.Core.UseCases.ConfirmOrder;
using iBurguer.Ordering.Core.UseCases.CancelOrder;
using System.Diagnostics.CodeAnalysis;

namespace iBurguer.Ordering.Infrastructure.SQS
{
    [ExcludeFromCodeCoverage]
    public abstract class SQSWorker : BackgroundService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IServiceScopeFactory _scopeFactory;
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

        protected IAmazonSQS CreateClient(SQSConfiguration configuration)
        {
            var accessKey = configuration.AccessKey;
            var secretKey = configuration.SecretKey;
            var region = RegionEndpoint.USEast1;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            return new AmazonSQSClient(credentials, region);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configuration = _configuration.GetRequiredSection("MassTransit").Get<SQSConfiguration>();

            using IServiceScope serviceScope = _scopeFactory.CreateScope();

            _registerOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<IRegisterOrderUseCase>();
            _confirmOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<IConfirmOrderUseCase>();
            _cancelOrderUseCase = serviceScope.ServiceProvider.GetRequiredService<ICancelOrderUseCase>();

            var client = CreateClient(configuration);

            var queueName = GetQueue(configuration);
            var queueUrl = await GetQueueUrl(client, queueName);

            await Start(client, queueUrl, stoppingToken);

        }

        private async Task Start(IAmazonSQS client, string queueUrl, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Starting polling queue");

            while (!cancellationToken.IsCancellationRequested)
            {
                var messages = await ReceiveMessageAsync(client, queueUrl);

                if (messages.Any())
                {
                    Console.WriteLine($"{messages.Count()} messages received");

                    foreach (var msg in messages)
                    {
                        await Handle(msg, cancellationToken);
                        await DeleteMessageAsync(client, queueUrl, msg.ReceiptHandle);
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }
        }

        protected static async Task<string> GetQueueUrl(IAmazonSQS client, string queueName)
        {
            var response = await client.GetQueueUrlAsync(new GetQueueUrlRequest
            {
                QueueName = queueName
            });

            return response.QueueUrl;
        }

        protected static async Task<List<Message>> ReceiveMessageAsync(IAmazonSQS client, string queueUrl)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 10
            };

            var messages = await client.ReceiveMessageAsync(request);

            return messages.Messages;
        }

        protected static async Task DeleteMessageAsync(IAmazonSQS client, string queueUrl, string id)
        {
            var request = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = id
            };

            await client.DeleteMessageAsync(request);
        }
    }
}
