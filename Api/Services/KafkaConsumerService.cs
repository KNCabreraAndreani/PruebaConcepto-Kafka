using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using System.Text;

namespace Api.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly ClusterClient _cluster;
        public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
        {

            _logger = logger;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            await Task.Run(() =>
            {
                _cluster.ConsumeFromLatest("demo");
                _cluster.MessageReceived += record =>
                {
                    _logger.LogInformation($"Received: {Encoding.UTF8.GetString(record.Value as byte[])}");
                };
            }, cancellationToken);

            await Task.CompletedTask;
            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }
    }
}
