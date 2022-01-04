using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Connector.Contracts
{
    public interface IConfig
    {
        Task GetProperties();

        Task SetCustomConfig(Models.Configuration configProperties);

        Task<ProducerConfig> CreateProducerConfig();

        Task<ConsumerConfig> CreateConsumerConfig();
    }
}
