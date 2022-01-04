using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfraService.MongoDb
{
    public interface IRepositorio<T> where T : class
    {
        void Add(T entity);
        void Delete(string id);
        Task<IEnumerable<T>> GetAll();
        void Dispose();
    }
}
