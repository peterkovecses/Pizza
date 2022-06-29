using Microsoft.AspNetCore.Mvc;
using Pizza.Api.Controllers;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;
using Pizza.Data.Entities;
using Pizza.Tests.Helpers;

namespace Pizza.Tests
{
    public class ProductsControllerTests
    {
        private readonly IProductService _productService;
        private readonly ProductsController _controller;
        private readonly ProductQueryParameters _queryParameters;

        public ProductsControllerTests()
        {
            _productService = new FakeProductService();
            _controller = new ProductsController(_productService);
            _queryParameters = new ProductQueryParameters();
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
            var products = Assert.IsType<List<ProductDto>>((okResult as OkObjectResult).Value);
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
            var notFoundResult = await (_controller.DeleteProductAsync(id));

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
    }
}
