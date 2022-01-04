using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Connector.Contracts
{
    public interface IServerConnector
    {
        Task<ProducerConfig> CreateProducerInstanceConnetor();

        Task<ConsumerConfig> CreateConsumerInstanceConnetor();
    }
}
