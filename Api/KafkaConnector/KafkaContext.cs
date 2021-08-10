using Api.Models;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.KafkaConnector
{
    public class KafkaContext : IKafkaContext<int, string>
    {
        private IProducer<int, string> _producer;

        public KafkaContext()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<int, string>(config).Build();
        }

        public IProducer<int, string> GetProducer { get { return _producer; } }
    }
}
