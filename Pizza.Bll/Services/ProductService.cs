using Pizza.Bll.Dtos;
using Pizza.Data.Entities;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;
using Pizza.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ProductDto> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.SingleAsync(p => p.Id == id);

            product.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids)
        {
            var productsToDelete = _dbContext.Products.Where(p => ids.Contains(p.Id));
            await productsToDelete.ForEachAsync(p => p.IsDeleted = true);            

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(productsToDelete);
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var product = await _dbContext.Products.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync_V1_0(ProductQueryParameters queryParameters)
        {
            var products = await _dbContext.Products
                .Where(p => p.IsDeleted == false)
                .FilterCategory(queryParameters.CategoryId)
                .FilterByPrice(queryParameters.MinPrice, queryParameters.MaxPrice)
                .SearchByTerm(queryParameters.SearchTerm)
                .OrderProductByCustom(queryParameters.SortBy, queryParameters.SortOrder)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync_V2_0(ProductQueryParameters queryParameters)
        {
            if (queryParameters.PageNumber == 0)
                queryParameters.PageNumber = 1;

            if (queryParameters.PageSize == 0)
                queryParameters.PageSize = 25;

            var products = await _dbContext.Products
                .Where(p => p.IsDeleted == false)
                .FilterCategory(queryParameters.CategoryId)
                .FilterByPrice(queryParameters.MinPrice, queryParameters.MaxPrice)
                .SearchByTerm(queryParameters.SearchTerm)
                .OrderProductByCustom(queryParameters.SortBy, queryParameters.SortOrder)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task<bool> IsProductExists(int id)
        {
            return await _dbContext.Products.AnyAsync(b => b.Id == id);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var productInDb = await _dbContext.Products.SingleAsync(p => p.Id == productDto.Id);

            _mapper.Map(productDto, productInDb);

            await _dbContext.SaveChangesAsync();
        }
    }
}
