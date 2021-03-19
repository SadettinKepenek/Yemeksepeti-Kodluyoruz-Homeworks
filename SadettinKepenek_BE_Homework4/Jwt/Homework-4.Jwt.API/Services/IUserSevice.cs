using System.Threading.Tasks;
using Homework_4.Jwt.API.Models;

namespace Homework_4.Jwt.API.Services
{
    public interface IUserService
    {
        Task<UserInfo> Authenticate(TokenRequest req);

    }
}