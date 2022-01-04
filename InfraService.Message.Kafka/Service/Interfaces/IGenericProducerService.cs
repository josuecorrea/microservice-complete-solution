using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface IGenericProducerService<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}
