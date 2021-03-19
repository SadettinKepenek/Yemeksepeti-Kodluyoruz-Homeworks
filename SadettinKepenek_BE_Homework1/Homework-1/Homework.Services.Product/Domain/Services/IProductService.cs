using System.Collections.Generic;
using System.Threading.Tasks;
using Homework.Services.Product.Domain.Models;
using Homework.Services.Product.Domain.Responses;

namespace Homework.Services.Product.Domain.Services
{
    public interface IProductService
    {
        Task<ServiceResponseModel> Create(ProductDto productDto);
        Task<List<ProductDto>> GetAll();
        ProductDto Get(int id);
        ServiceResponseModel Update(ProductDto productDto);
        ServiceResponseModel Delete(int id);
    }
}