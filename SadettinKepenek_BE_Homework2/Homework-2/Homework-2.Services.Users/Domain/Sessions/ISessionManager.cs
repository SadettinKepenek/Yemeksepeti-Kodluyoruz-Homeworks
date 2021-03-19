namespace Homework_2.Services.Users.Domain.Sessions
{
    public interface ISessionManager
    {
        void SetSession(string sessionName,string value);
        string GetSession(string sessionName);
    }
}