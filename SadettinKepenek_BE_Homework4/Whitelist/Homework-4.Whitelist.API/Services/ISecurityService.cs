namespace Homework_4.Whitelist.API.Services
{
    public interface ISecurityService
    {
        bool CanAccessController(string ipAddress,string controllerName);
    }
}