using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfraService.MongoDb
{
    public class BaseRepository<T> : IRepositorio<T>, IDisposable where T : class, new()
    {
        private MongoDBContext<T> _dbContext;

        public BaseRepository(MongoDBContext<T> dbContext) => _dbContext = dbContext;

        public virtual async void Add(T entity) => await _dbContext.GetColection.InsertOneAsync(entity);

        public async void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _dbContext.GetColection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var documents = await _dbContext.GetColection.FindAsync(Builders<T>.Filter.Empty);

            return await documents.ToListAsync();
        }

        public void Dispose() => GC.SuppressFinalize(this);

    }
}
