using Pizza.Bll.Dtos;
using Pizza.Data.Entities;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;
using Pizza.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Pizza.Bll.Services
{
    public class ProductService : IProductService
    {
        private readonly PizzaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ProductService> _logger;
        private readonly Stopwatch _stopwatch;

        public ProductService(PizzaDbContext dbContext, IMapper mapper, IMemoryCache memoryCache, ILogger<ProductService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
            _stopwatch = new Stopwatch();
            _dbContext.Database.EnsureCreated();
        }

        private readonly Expression<Func<Product, ProductDto>> _productSelector = p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            PhotoPath = p.PhotoPath,
            CategoryId = p.CategoryId
        };

        private void LogProductServiceStarted()
        {
            _logger.LogInformation("Product Background Service - Start");
            _stopwatch.Start();
        }

        private void LogProductServiceEnded()
        {
            _stopwatch?.Stop();
            _logger.LogInformation($"Product Background Service finished in {_stopwatch.Elapsed.TotalSeconds} seconds.");
        }

        public async Task<int> CreateProductAsync(ProductDto productDto)
        {
            LogProductServiceStarted();

            var product = _mapper.Map<ProductDto, Product>(productDto);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            LogProductServiceEnded();
            return product.Id;
        }

        public async Task<ProductDto> DeleteProductAsync(int id)
        {
            LogProductServiceStarted();

            var product = await _dbContext.Products.SingleAsync(p => p.Id == id);

            product.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            LogProductServiceEnded();

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids)
        {
            LogProductServiceStarted();

            var productsToDelete = _dbContext.Products.Where(p => ids.Contains(p.Id));
            await productsToDelete.ForEachAsync(p => p.IsDeleted = true);            

            await _dbContext.SaveChangesAsync();

            LogProductServiceEnded();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(productsToDelete);
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            LogProductServiceStarted();

            var product = _memoryCache.Get<Product>(id);

            if (product == null)
            {
                product = await _dbContext.Products.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _memoryCache.Set(id, product, cacheEntryOptions);
            }

            LogProductServiceEnded();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync_V1_0(ProductQueryParameters queryParameters)
        {
            LogProductServiceStarted();

            var products = await _dbContext.Products
                .Where(p => p.IsDeleted == false)
                .FilterCategory(queryParameters.CategoryId)
                .FilterByPrice(queryParameters.MinPrice, queryParameters.MaxPrice)
                .SearchByTerm(queryParameters.SearchTerm)
                .OrderProductByCustom(queryParameters.SortBy, queryParameters.SortOrder)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            LogProductServiceEnded();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }
        public async Task<PagedList<ProductDto>> GetProductsAsync_V2_0(ProductQueryParameters queryParameters)
        {
            LogProductServiceStarted();

            var products = await _dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Select(_productSelector)
                .FilterCategory(queryParameters.CategoryId)
                .FilterByPrice(queryParameters.MinPrice, queryParameters.MaxPrice)
                .SearchByTerm(queryParameters.SearchTerm)
                .OrderProductByCustom(queryParameters.SortBy, queryParameters.SortOrder)
                .ToPagedListAsync<ProductDto>(queryParameters.PageNumber, queryParameters.PageSize);

            LogProductServiceEnded();

            return products;
        }

        public async Task<bool> IsProductExists(int id)
        {
            LogProductServiceStarted();

            var exists = await _dbContext.Products.AnyAsync(b => b.Id == id);

            LogProductServiceEnded();

            return exists;
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            LogProductServiceStarted();

            var productInDb = await _dbContext.Products.SingleAsync(p => p.Id == productDto.Id);

            _mapper.Map(productDto, productInDb);

            await _dbContext.SaveChangesAsync();

            LogProductServiceEnded();
        }
    }
}
