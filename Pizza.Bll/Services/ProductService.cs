using Pizza.Bll.Dtos;
using Pizza.Data.Entities;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;
using Pizza.Data;
using AutoMapper;

namespace Pizza.Bll.Services
{
    public class ProductService : IProductService
    {
        private readonly PizzaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductService(PizzaDbContext dbContext, IMapper mapper, ICategoryService categoryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _categoryService = categoryService;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<int> CreateProductAsync(ProductDto productDto)
        {
            if (!_categoryService.IsCategoryExists(productDto.CategoryId).Result)
                return 0;

            var product = _mapper.Map<ProductDto, Product>(productDto);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product.Id;
        }

        public Task<ProductDto> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
