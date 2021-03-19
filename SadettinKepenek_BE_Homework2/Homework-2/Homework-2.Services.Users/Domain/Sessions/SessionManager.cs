
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Homework_2.Services.Users.Domain.Sessions
{
    
    public class SessionManager:ISessionManager
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public void SetSession(string sessionName, string value)
        {
            _httpContextAccessor.HttpContext.Session.Set(sessionName,Encoding.UTF8.GetBytes(value));
        }

        public string GetSession(string sessionName)
        {
            _httpContextAccessor.HttpContext.Session.TryGetValue(sessionName,out var data);
            return Encoding.UTF8.GetString(data);
        }
    }
}