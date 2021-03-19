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
    public class CustomerService:ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly OrderDbContext _dbContext;

        public CustomerService(IMapper mapper, OrderDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        
        public async Task<ServiceResponseModel> Create(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponseModel("Customer has been created successfully",true);
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            var customersDto = _mapper.Map<List<CustomerDto>>(customers);
            return customersDto;
        }

        public async Task<CustomerDto> GetById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
    }
}