using Confluent.Kafka;
using InfraService.Message.Kafka.Connector.Contracts;
using System;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Connector.Implements
{
    public class Config : IConfig
    {
        //private readonly IConfiguration _configuration;

        //public Config(IConfiguration configuration)
        //{
        //    _configuration = configuration;

        //    var builder = new ConfigurationBuilder()
        //    .SetBasePath(@"C:\config\")//TODO: COLOCAR VARIAÇÃO PARA LINUX
        //    .AddJsonFile("kafka-config.json", optional: true, reloadOnChange: true);

        //    _configuration = builder.Build();
        //}

        public Task GetProperties()
        {
            throw new NotImplementedException();
        }

        public Task SetCustomConfig(Models.Configuration configProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<ProducerConfig> CreateProducerConfig()
        {
            var config = new ProducerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "localhost:9092";
                //config.SslCaLocation = "/Path-to/cluster-ca-certificate.pem";
                //config.SecurityProtocol = SecurityProtocol.SaslSsl;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG

            });

            return config;
        }

        public async Task<ConsumerConfig> CreateConsumerConfig()
        {
            var config = new ConsumerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "localhost:9092";
                //config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.AutoOffsetReset = AutoOffsetReset.Earliest;
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG


            });

            return config;
        }
    }
}
