using Confluent.Kafka;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface IProducerService
    {
        Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message);
    }
}
