using Microsoft.EntityFrameworkCore;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;

namespace Pizza.Tests.Helpers
{
    internal class FakeProductService : IProductService
    {
        private List<ProductDto> _products;

        public FakeProductService()
        {
            _products = TestDataProvider.GetTestDataForControllerTests();
        }

        public async Task<int> CreateProductAsync(ProductDto productDto)
        {
            productDto.Id = _products.Max(p => p.Id);
            _products.Add(productDto);
            return await Task.FromResult(productDto.Id);
        }

        public async Task<ProductDto> DeleteProductAsync(int id)
        {
            var productToRemove = _products.First(p => p.Id == id);
            _products.Remove(productToRemove);
            return await Task.FromResult(productToRemove);
        }

        public async Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids)
        {
            var productsToDelete = _products.Where(p => ids.Contains(p.Id)).ToList();
            productsToDelete.ForEach(p => _products.Remove(p));
            return await Task.FromResult(productsToDelete);
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            return await Task.FromResult(_products.SingleOrDefault(p => p.Id == id));
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync_V1_0(ProductQueryParameters queryParameters)
        {
            return await Task.FromResult(_products);
        }

        public async Task<PagedList<ProductDto>> GetProductsAsync_V2_0(ProductQueryParameters queryParameters)
        {
            var products = _products.ToPagedList<ProductDto>(queryParameters.PageNumber, queryParameters.PageSize);
            return await Task.FromResult(products);
        }

        public async Task<bool> IsProductExists(int id)
        {
            return await Task.FromResult(_products.Any(p => p.Id == id));
        }

        public async Task<bool> AreProductsExists(int[] ids)
        {
            return await Task.FromResult(_products.Where(p => ids.Contains(p.Id)).Count() == ids.Distinct().Count());
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            if (!_products.Any(p => p.Id == productDto.Id))
                throw new DbUpdateConcurrencyException();

            var productToUpdate = await Task.FromResult(_products.Single(p => p.Id == productDto.Id));
            productToUpdate.Name = productDto.Name;
            productToUpdate.Description = productDto.Description;
            productToUpdate.Price = productDto.Price;
            productToUpdate.Photo = productDto.Photo;
            productToUpdate.PhotoPath = productDto.PhotoPath;
            productToUpdate.CategoryId = productToUpdate.CategoryId;
        }
    }
}
