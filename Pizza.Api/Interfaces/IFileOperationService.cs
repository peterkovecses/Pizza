using Pizza.Api.Helpers;
using Pizza.Bll.Dtos;

namespace Pizza.Api.Interfaces
{
    public interface IFileOperationService
    {
        public Task<(string, FileErrorType?)> SaveProductPhotoAsync(ProductDto barber);
    }
}
