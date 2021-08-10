using Api.KafkaConnector;
using Api.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class OrdenesService : IOrdenesService
    {
        private readonly ILogger<OrdenesService> _logger;
        private readonly IKafkaContext<int, string> _kafkaContext;

        public OrdenesService(ILogger<OrdenesService> logger, IKafkaContext<int,string> kafkaContext)
        {
            _logger = logger;
            _kafkaContext = kafkaContext;
        }

        public async Task GenerarMensajeKafka(MessageOrden message)
        {
            _logger.LogInformation($"Llego el mensaje {message.Mensaje}");
            await _kafkaContext.GetProducer.ProduceAsync("demo", new Message<int, string>()
            {
                Key = message.Id,
                Value = message.Mensaje
            });
            _logger.LogInformation("Mensaje enviado.");

            await Task.CompletedTask;
        }
    }
}
