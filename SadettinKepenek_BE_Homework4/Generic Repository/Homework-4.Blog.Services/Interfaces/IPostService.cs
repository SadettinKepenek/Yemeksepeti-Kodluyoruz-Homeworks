using System.Collections.Generic;
using System.Threading.Tasks;
using Homework_4.Blog.Domain.Models;

namespace Homework_4.Blog.Services.Interfaces
{
    public interface IPostService
    {
        Task<ServiceResponseModel> Create(PostDto post);
        Task<ServiceResponseModel> Update(PostDto post);
        Task<ServiceResponseModel> Delete(int id);
        Task<List<PostDto>> GetAll();
        Task<PostDto> GetById(int id);
        
    }
}