using Api.Models;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.KafkaConnector
{
    public interface IKafkaContext<T,TH>
    {
        public IProducer<T, TH> GetProducer { get; }
    }
}
