using Pizza.Api.Helpers;
using Pizza.Api.Interfaces;
using Pizza.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Tests.Helpers
{
    internal class FakeFileOperationService : IFileOperationService
    {
        public Task<(string, FileErrorType?)> SaveProductPhotoAsync(ProductDto barber)
        {
            throw new NotImplementedException();
        }
    }
}
