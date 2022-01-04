using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Connector.Implements
{
    public class ServerConnector : IServerConnector
    {
        private readonly IConfig _config;

        public ServerConnector(IConfig config)
        {
            _config = config;
        }

        public async Task<ConsumerConfig> CreateConsumerInstanceConnetor() => await _config.CreateConsumerConfig();

        public async Task<ProducerConfig> CreateProducerInstanceConnetor() => await _config.CreateProducerConfig();
    }
}
