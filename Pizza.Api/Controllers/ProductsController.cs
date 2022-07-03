using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza.Api.Helpers;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;

namespace Pizza.Api.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ICategoryService categoryService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [ApiVersion("1.0")]
        [HttpGet]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "MinPrice", "MaxPrice", "SearchTerm", "CategoryId", "PageNumber", "PageSize", "SortBy", "SortOrder" })]
        public async Task<IActionResult> GetProductsAsync_V1_0([FromQuery] ProductQueryParameters queryParameters)
        {
            _logger.LogInformation($"Run endpoint /api/product Get ");

            var products = await _productService.GetProductsAsync_V1_0(queryParameters);

            var response = Ok(products);            

            return Ok(products);
        }

        [ApiVersion("2.0")]
        [HttpGet]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "MinPrice", "MaxPrice", "SearchTerm", "CategoryId", "PageNumber", "PageSize", "SortBy", "SortOrder" })]
        public async Task<IActionResult> GetProductsAsync_V2_0([FromQuery] ProductQueryParameters queryParameters)
        {   
            _logger.LogInformation($"Run endpoint /api/product Get ");

            var products = await _productService.GetProductsAsync_V2_0(queryParameters);

            Response.AddPaginationHeader(products.CurrentPage, products.PageSize,
                products.TotalItems, products.TotalPages);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            _logger.LogInformation($"Run endpoint /api/product/{id} Get ");

            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                _logger.LogTrace($"Failed to get Product entity with Id {product.Id}, because it doesn't exist.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductDto productDto)
        {
            _logger.LogInformation("Run endpoint /api/product POST ");

            if (!ModelState.IsValid)
            {
                _logger.LogTrace($"Failed to create Product, because ModelState not valid.");
                return BadRequest(ModelState);
            }

            if (!_categoryService.IsCategoryExists(productDto.CategoryId).Result)
            {
                _logger.LogTrace($"Failed to create Product, because Category with Id {productDto.CategoryId} doesn't exist.");
                return NotFound("The specified category does not exist.");
            }

            productDto.Id = await _productService.CreateProductAsync(productDto);

            _logger.LogTrace($"Added new Product entity with Id {productDto.Id}");

            return CreatedAtAction("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, ProductDto productDto)
        {
            _logger.LogInformation($"Run endpoint /api/product/{id} PUT ");

            if (!ModelState.IsValid)
            {
                _logger.LogTrace($"Failed to upgrade Product entity with Id {productDto.Id}, because ModelState not valid.");
                return BadRequest(ModelState);
            }
            if (id != productDto.Id)
            {
                _logger.LogTrace($"Failed to upgrade Product entity with Id {productDto.Id}, because Ids are not the same.");
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.UpdateProductAsync(productDto);

                _logger.LogTrace($"Succesfully updated Product entity with Id {productDto.Id}");

                return Ok(productDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productService.IsProductExists(id))
                {
                    _logger.LogTrace($"Failed to update Product entity with Id {productDto.Id}, because Product doesn't exist.");
                    return NotFound();
                }
                else
                {
                    _logger.LogTrace($"Failed to update Product entity with Id {productDto.Id} for an unknown reason");
                    return BadRequest();
                }                
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            _logger.LogInformation($"Run endpoint /api/product/{id} Delete ");

            if (!await _productService.IsProductExists(id))
            {
                _logger.LogTrace($"Failed to delete Product entity with Id {id}, because Product doesn't exist.");
                return NotFound();
            }

            var product = await _productService.DeleteProductAsync(id);

            _logger.LogTrace($"Succesfully deleted Product entity with Id {product.Id}");

            return Ok(product);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProductsAsync([FromQuery] int[] ids)
        {
            _logger.LogInformation($"Run endpoint /api/product/Delete Post ");

            foreach (var id in ids)
            {
                if (!await _productService.IsProductExists(id))
                    return NotFound();
            }

            var products = await _productService.DeleteProductsAsync(ids);

            _logger.LogTrace($"Succesfully deleted Product entities.");

            return Ok(products);
        }
    }
}
