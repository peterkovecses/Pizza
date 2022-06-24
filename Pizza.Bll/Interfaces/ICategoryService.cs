using Pizza.Bll.Dtos;

namespace Pizza.Bll.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoryAsync();
        Task<CategoryDto> GetCategoryAsync(int id);
        Task<int> CreateCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryDto>> DeleteCategoryAsync(int[] ids);
        Task<bool> IsCategoryExists(int id);
    }
}
