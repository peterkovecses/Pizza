using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;

namespace Pizza.Bll.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters queryParameters);
        Task<ProductDto> GetProductAsync(int id);
        Task<int> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);
        Task<ProductDto> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> DeleteProductsAsync(int[] ids);
        Task<bool> IsProductExists(int id);
    }
}
