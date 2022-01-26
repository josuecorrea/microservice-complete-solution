using InfraService.SQLServer.Contracts;

namespace InfraService.SQLServer.Implements
{
    internal class DataSettings : IDataSettings
    {
        public string DefaultConnection { get; set; }
    }
}
