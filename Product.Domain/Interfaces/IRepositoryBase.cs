using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<int> Add(T entity);   
        Task AddAsNoTracking(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> filter);
    }
}
