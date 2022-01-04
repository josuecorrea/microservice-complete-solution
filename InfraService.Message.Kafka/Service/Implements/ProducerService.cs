using Confluent.Kafka;
using InfraService.Message.Kafka.Connector.Contracts;
using InfraService.Message.Kafka.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Implements
{
    public class ProducerService : IProducerService, IDisposable
    {
        private readonly IServerConnector _serverConnectorFactory;
        IProducer<Null, string> _producer;

        public ProducerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;

            _producer = new ProducerBuilder<Null, string>(_serverConnectorFactory.CreateProducerInstanceConnetor().GetAwaiter().GetResult()).Build();
        }

        public async Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message)
        {
            //using var producer = new ProducerBuilder<Null, string>(await _serverConnectorFactory.CreateProducerInstanceConnetor()).Build();

            try
            {
                var sendResult = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

                return sendResult;
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
