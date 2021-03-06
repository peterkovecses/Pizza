using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;

namespace Pizza.Bll.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync_V1_0(ProductQueryParameters queryParameters);
        Task<PagedList<ProductDto>> GetProductsAsync_V2_0(ProductQueryParameters queryParameters);
        Task<ProductDto> GetProductAsync(int id);
        Task<int> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);
        Task<ProductDto> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids);
        Task<bool> IsProductExists(int id);
        Task<bool> AreProductsExists(int[] ids);

    }
}
