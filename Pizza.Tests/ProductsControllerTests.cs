using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pizza.Api.Controllers;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;
using Pizza.Data.Entities;
using Pizza.Tests.Helpers;

namespace Pizza.Tests
{
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private readonly ProductQueryParameters _queryParameters;

        public ProductsControllerTests()
        {          
            SetController();
            _queryParameters = new ProductQueryParameters();
        }

        private void SetController()
        {
            var productService = new FakeProductService();
            var categoryService = new FakeCategoryService();
            var fileOperationService = new FakeFileOperationService();
            var logger = Mock.Of<ILogger<ProductsController>>();
            _controller = new ProductsController(productService, categoryService, fileOperationService, logger);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public async Task GetProductsAsync_V1_0_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetProductsAsync_V1_0(_queryParameters);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task GetProductsAsync_V2_0_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetProductsAsync_V2_0(_queryParameters);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task GetProductsAsync_V1_0_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.GetProductsAsync_V1_0(_queryParameters);

            // Assert
            var products = Assert.IsType<List<ProductDto>>((okResult as OkObjectResult).Value);
            Assert.Equal(19, products.Count);
        }

        [Fact]
        public async Task GetProductsAsync_V2_0_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.GetProductsAsync_V2_0(_queryParameters);

            // Assert
            var products = Assert.IsType<PagedList<ProductDto>>((okResult as OkObjectResult).Value);
            Assert.Equal(19, products.Count);
        }

        [Fact]
        public async Task GetProductAsync_WhenCalled_ReturnsItemWithSameId()
        {
            // Arrange
            var id = 1;

            // Act
            var okResult = await (_controller.GetProductAsync(id));

            // Assert
            var product = Assert.IsType<ProductDto>((okResult as OkObjectResult).Value);
            Assert.Equal(id, product.Id);
        }

        [Fact]
        public async Task GetProductAsync_IfThereIsNoProductWithThePassedId_ReturnsNotFound()
        {
            // Arrange
            var id = 111;

            // Act
            var notFoundResult = await (_controller.GetProductAsync(id));

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task CreateProductAsync_WhenModelStateIsNotValid_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var product = new ProductDto();
            _controller.ModelState.AddModelError("TestError", "TestError");

            // Act
            var badRequestResult = await (_controller.CreateProductAsync(product));

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
            _controller.ModelState.Clear();
        }

        [Fact]
        public async Task CreateProductAsync_WhenCategoryIdNotExists_ReturnsNotFoundtObjectResult()
        {
            // Arrange
            var product = new ProductDto { Id = 19, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "productImages/19.jpg", Price = 35, CategoryId = 14 };

            // Act
            var notFoundResult = await (_controller.CreateProductAsync(product));
            var result = notFoundResult as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
            Assert.Equal("The specified category does not exist.", result.Value);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdsNotTheSame_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var product = new ProductDto { Id = 19, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "productImages/19.jpg", Price = 35, CategoryId = Category.Wine };

            // Act
            var badRequestResult = await (_controller.UpdateProductAsync(18, product));

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdNotFound_ReturnsNotFound()
        {
            // Arrange
            var product = new ProductDto { Id = 188, Name = "Canyon Road Cabernet", Description = "California", PhotoPath = "productImages/19.jpg", Price = 35, CategoryId = Category.Wine };

            // Act
            var notFoundResult = await (_controller.UpdateProductAsync(188, product));

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteProductAsync_WhenIdNotFound_ReturnsNotFound()
        {
            // Arrange
            var id = 77;

            // Act
            var notFoundResult = await _controller.DeleteProductAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteProductsAsync_WhenOneIdNotFound_ReturnsNotFound()
        {
            // Arrange
            var ids = new int[] { 1, 2, 87 };

            // Act
            var notFoundResult = await _controller.DeleteProductsAsync(ids);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task DeleteProductsAsync_WhenParameterIdsAreAllTheSame_DeletesOnlyOneProduct()
        {
            // Arrange
            var ids = new int[] { 1, 1, 1 };

            // Act            
            var result = await _controller.DeleteProductsAsync(ids) as ObjectResult;
            var deletedProducts = result.Value as IEnumerable<ProductDto>;
            var actual = deletedProducts.Count();

            // Assert
            Assert.Equal(1, actual);
        }
    }
}
