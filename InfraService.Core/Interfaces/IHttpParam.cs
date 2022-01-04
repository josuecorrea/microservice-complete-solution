namespace InfraService.Core.Interfaces
{
    public interface IHttpParam
    {
        string GetApiUrl();
        string GetToken();
        bool IsAuthentication();
    }
}
