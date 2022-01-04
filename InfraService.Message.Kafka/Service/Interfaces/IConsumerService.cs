using System.Threading;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface IConsumerService
    {
        Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken);
    }
}
