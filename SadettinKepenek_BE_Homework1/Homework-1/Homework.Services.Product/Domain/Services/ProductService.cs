using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Homework.Services.Product.Domain.Mappers;
using Homework.Services.Product.Domain.Models;
using Homework.Services.Product.Domain.Repositories;
using Homework.Services.Product.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Homework.Services.Product.Domain.Services
{
    public sealed class ProductService : IProductService
    {
        private IMapper _mapper;
        private IRepository<Entities.Product, int> _productRepository;
        private List<Entities.Product> _productsInMemory = new List<Entities.Product>();
        private static ProductService instance;

        public static ProductService GetInstance(IMapper mapper, IRepository<Entities.Product, int> productRepository)
        {
            if (instance != null)
            {
                instance._productRepository = productRepository;
                instance._mapper = mapper;
                return instance;
            }

            instance = new ProductService(mapper, productRepository);
            return instance;
        }

        private ProductService(IMapper mapper, IRepository<Entities.Product, int> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ServiceResponseModel> Create(ProductDto productDto)
        {
            var product = _mapper.Map<Entities.Product>(productDto);
            await _productRepository.AddAsync(product);
            _productsInMemory.Add(product);
            return new ServiceResponseModel("Product created", true);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products =  _productsInMemory.Count == 0 ?  await _productRepository.GetAllAsync() : _productsInMemory;
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }

        public ProductDto Get(int id)
        {
            var product = _productsInMemory.FirstOrDefault(p => p.Id == id);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public ServiceResponseModel Update(ProductDto productDto)
        {
            if (productDto.Id == 0)
            {
                return new ServiceResponseModel("Id cannot be null", false);
            }

            var productInMemory = _productsInMemory
                .FirstOrDefault(p => p.Id == productDto.Id);
            
            var productToBeUpdated = productInMemory ?? _productRepository
                .GetQueryable()
                .FirstOrDefault(p => p.Id == productDto.Id);
            
            if (productToBeUpdated == null)
            {
                return new ServiceResponseModel("Cannot find product", false);
            }

            var product = _mapper.Map<Entities.Product>(productDto);
            _productRepository.Update(product);
            if (productInMemory == null)
            {
                _productsInMemory.Add(product);
            }

            UpdateProductInMemory(productDto, product);

            return new ServiceResponseModel("Product updated", true);
        }

        private void UpdateProductInMemory(ProductDto productDto, Entities.Product product)
        {
            var productInMemory = _productsInMemory.FirstOrDefault(p => p.Id == productDto.Id);
            var idx = _productsInMemory.IndexOf(productInMemory);
            _productsInMemory[idx] = product;
        }

        public  ServiceResponseModel Delete(int id)
        {
            if (id == 0)
            {
                return new ServiceResponseModel("Id cannot be null", false);
            }

            var productInMemory = _productsInMemory.FirstOrDefault(p => p.Id == id);

            var product = productInMemory ??
                           _productRepository.GetQueryable().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new ServiceResponseModel("Cannot find product", false);
            }

            _productRepository.Remove(product);
            _productsInMemory.Remove(product);
            return new ServiceResponseModel("Product deleted", true);
        }
    }
}