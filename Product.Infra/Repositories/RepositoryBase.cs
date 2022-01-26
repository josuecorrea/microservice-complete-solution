using InfraService.SQLServer;
using InfraService.SQLServer.Contracts;
using Product.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data.SqlClient;

namespace Product.Infra.Repositories
{
    internal class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class
    {
        protected readonly IDbConnection dbConn;
        protected readonly IDbTransaction dbTransaction;

        private const string ASSEMBLY_BASE = "Product.Infra";


        public RepositoryBase(IDatabaseFactory databaseFactory)
        {
            dbConn = databaseFactory.GetDbConnection;
            dbConn.Open();
        }

        public RepositoryBase(IDbConnection dbConnection, IDbTransaction transaction = null)
        {
            dbConn = dbConnection;
            if (dbConn.State != ConnectionState.Open)
            {
                dbConn.Open();
            }

            dbTransaction = transaction;
        }

        public async Task<int> Add(T entity)
        {
            try
            {
                var type = entity.GetType().Name;

                var sql = await FileResource.GetFileResourceAsync(ASSEMBLY_BASE, type, ScriptType.Insert);

                var result = await dbConn.QueryAsync<int>(sql);

                return result.First();
            }
            catch (SqlException ex) when (dbConn.State != ConnectionState.Open)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public Task AddAsNoTracking(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
