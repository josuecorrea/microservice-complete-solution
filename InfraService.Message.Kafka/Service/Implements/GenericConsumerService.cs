using Confluent.Kafka;
using InfraService.Message.Kafka.Connector.Contracts;
using InfraService.Message.Kafka.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Implements
{
    public class GenericConsumerService<TKey, TValue> : IGenericConsumerService<TKey, TValue> where TValue : class
    {
        private readonly IServerConnector _serverConnectorFactory;

        public delegate Task MessageConsumedDelegate(Message<TKey, TValue> message);

        public event MessageConsumedDelegate OnMessageConsumed;

        public GenericConsumerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;
        }

        public async Task Consume(string topic, string groupId, CancellationToken stoppingToken)
        {
            await Task.Run(() => StartConsumerLoop(topic, groupId, stoppingToken), stoppingToken);
        }

        private async Task StartConsumerLoop(string topic, string groupId, CancellationToken cancellationToken)
        {
            var consumerConfig = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            consumerConfig.GroupId = groupId;

            var consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig).SetValueDeserializer(new JsonDeserializer<TValue>()).Build();

            consumer.Subscribe(topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(cancellationToken);

                    if (result != null)
                    {
                        await OnMessageConsumed.Invoke(result.Message);

                        //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
                        if (!true)
                        {
                            consumer.Commit(result);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }
    }
}
