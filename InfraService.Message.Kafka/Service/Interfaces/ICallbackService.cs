using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface ICallbackService
    {
        Task Message(Kafka.Models.Message message);
        Task Message(IEnumerable<Kafka.Models.Message> messages);
    }
}
