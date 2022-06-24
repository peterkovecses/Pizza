using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pizza.Bll.Dtos;
using Pizza.Bll.Interfaces;
using Pizza.Data;

namespace Pizza.Bll.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PizzaDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(PizzaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

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
            return await _dbContext.Categories.AnyAsync(b => b.Id == id);
        }

        public Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
