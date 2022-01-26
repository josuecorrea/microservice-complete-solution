using System.Data;

namespace InfraService.SQLServer.Contracts
{
    public interface IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
