using System.Threading;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface IGenericConsumerService<TKey, TValue>
    {
        Task Consume(string topic, string groupId, CancellationToken stoppingToken);

    }
}
