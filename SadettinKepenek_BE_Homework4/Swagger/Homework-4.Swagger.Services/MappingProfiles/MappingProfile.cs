using System;
using AutoMapper;
using Homework_4.Swagger.Infrastructure.Entities;
using Homework_4.Swagger.Infrastructure.Models;

namespace Homework_4.Swagger.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();

            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.FullName
                    , opt => opt.MapFrom(src => String.Concat(src.Firstname, src.Lastname)));
        }
    }
}