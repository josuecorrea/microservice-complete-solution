using InfraService.SQLServer.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace InfraService.SQLServer.Implements
{
    internal class DatabaseFactory : IDatabaseFactory
    {
        private IOptions<DataSettings> _dataSettings;

        public IDbConnection GetDbConnection => throw new NotImplementedException();

        protected string ConnectionString => !string.IsNullOrEmpty(_dataSettings.Value.DefaultConnection) ?
                                                _dataSettings.Value.DefaultConnection : DataBaseConnection
                                                                                            .ConnectConfiguration
                                                                                            .GetConnectionString("DefaultConnection");

        public IDbConnection GetConnection => new SqlConnection(ConnectionString);

        public DatabaseFactory(IOptions<DataSettings> dataSettings)
        {
            this._dataSettings = dataSettings;
        }
    }
}
