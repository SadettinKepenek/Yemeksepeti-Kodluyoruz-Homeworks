using AutoMapper;
using Homework.Services.Product.Domain.Models;

namespace Homework.Services.Product.Domain.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Entities.Product>().ReverseMap();
        }
    }
}