using System.Collections.Generic;
using System.Threading.Tasks;
using Homework_4.Swagger.Infrastructure.Models;

namespace Homework_4.Swagger.Services.Services.Abstract
{
    public interface ICustomerService
    {
        Task<ServiceResponseModel> Create(CustomerDto dto);
        Task<List<CustomerDto>> GetAll();
        Task<CustomerDto> GetById(int id);
    }
}