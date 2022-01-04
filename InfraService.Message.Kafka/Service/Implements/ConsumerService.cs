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
    public class ConsumerService : IConsumerService
    {
        private readonly IServerConnector _serverConnectorFactory;

        public ConsumerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;
        }

        public async Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(5000);

                    if (message != null)
                    {
                        await callback.Message(new Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
                        {
                            consumer.Commit(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                consumer.Close();
            }
        }

        public async Task Consume(string topic, string groupId, ICallbackService callback, long quantity, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var cts = new CancellationTokenSource();

            var messages = new List<Models.Message>();

            try
            {
                for (int i = 0; i <= quantity; i++)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        messages.Add(new Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
                        {
                            consumer.Commit(message);
                        }
                    }
                }

                await callback.Message(messages);
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task AssignNewPartitionOffSet(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            consumer.Assign(new TopicPartitionOffset(topic, partition, offset));

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        await callback.Message(new Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
                        {
                            consumer.Commit(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task OffSetConsumer(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartitionOffset(topic, partition, offset);

            var topicPartitionsOffsets = new List<TopicPartitionOffset>()
                    {
                        topicPartition,
                    };

            consumer.IncrementalAssign(topicPartitionsOffsets);

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        await callback.Message(new Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (!true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
                        {
                            consumer.Commit(message);
                        }
                    }

                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task<IEnumerable<TopicPartitionOffset>> GetCurrentCommitedOffSetFromTopic(string topic, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartition(topic, partition);

            var topicPartitions = new List<TopicPartition>()
                    {
                        topicPartition,
                    };

            var result = consumer.Committed(topicPartitions, TimeSpan.FromSeconds(10));

            return result;
        }


        public async Task<Offset> GetCurrentOffSetPositionFromTopic(string topic, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartition(topic, partition);

            var result = consumer.Position(topicPartition);

            return result;
        }


        public async Task StoreOffsets(string topic, long offset, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartitionOffset = new TopicPartitionOffset(topic, partition, offset);

            consumer.StoreOffset(topicPartitionOffset);
        }
    }
}
