using Pizza.Bll.Dtos;
using Pizza.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Tests.Helpers
{
    internal class FakeCategoryService : ICategoryService
    {
        private List<CategoryDto> _categories = new List<CategoryDto> 
        { 
            new CategoryDto { Id = 1, Name = "Chicago Deep Dish Pizza" },
            new CategoryDto { Id = 2, Name = "Chicago Thin Crust Pizza" },
            new CategoryDto { Id = 3, Name = "Beer" },
            new CategoryDto { Id = 4, Name = "Wine" }
        };

        public Task<int> CreateCategoryAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDto>> DeleteCategoryAsync(int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDto>> GetCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCategoryExists(int id)
        {
            return await Task.FromResult(_categories.Any(c => c.Id == id));
        }

        public Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
