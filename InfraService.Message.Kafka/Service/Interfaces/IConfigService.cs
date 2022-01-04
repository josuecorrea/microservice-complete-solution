using InfraService.Message.Kafka.Models;
using System.Threading.Tasks;

namespace InfraService.Message.Kafka.Service.Interfaces
{
    public interface IConfigService
    {
        Task SetCustomConfig(Configuration configProperties);
    }
}
