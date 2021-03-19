using System.Collections.Generic;
using System.Threading.Tasks;
using Homework_4.Blog.Domain.Models;

namespace Homework_4.Blog.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponseModel> Create(UserDto user);
        Task<ServiceResponseModel> Update(UserDto user);
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);

        Task<ServiceResponseModel> Delete(int id);
    }
}