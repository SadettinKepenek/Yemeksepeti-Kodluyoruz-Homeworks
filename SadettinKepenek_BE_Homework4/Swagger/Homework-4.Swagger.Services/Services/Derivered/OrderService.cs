using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Homework_4.Swagger.Infrastructure.Entities;
using Homework_4.Swagger.Infrastructure.Models;
using Homework_4.Swagger.Services.Data;
using Homework_4.Swagger.Services.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Swagger.Services.Services.Derivered
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly OrderDbContext _dbContext;

        public OrderService(IMapper mapper, OrderDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<ServiceResponseModel> Create(OrderDto dto)
        {
            var customer = _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == dto.Id);
            if (customer == null)
            {
                return new ServiceResponseModel($"Cannot find customer with id : {dto.Id}",false);
            }
            var order = _mapper.Map<Order>(dto);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponseModel("Order has been created successfully",true);
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            var ordersDto = _mapper.Map<List<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task<OrderDto> GetById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var order = await _dbContext.Orders.FirstOrDefaultAsync(c => c.Id == id);
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }
    }
}