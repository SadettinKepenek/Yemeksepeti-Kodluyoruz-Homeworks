using System.Collections.Generic;
using System.Threading.Tasks;
using Homework_4.Swagger.Infrastructure.Models;

namespace Homework_4.Swagger.Services.Services.Abstract
{
    public interface IOrderService
    {
        Task<ServiceResponseModel> Create(OrderDto dto);
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> GetById(int id);
    }
}