using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;
using Pizza.Bll.Interfaces;

namespace Pizza.Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductQueryParameters queryParameters)
        {
            var products = await _productService.GetProductsAsync(queryParameters);

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            productDto.Id = await _productService.CreateProductAsync(productDto);

            if (productDto.Id == 0)
                return NotFound("The specified category does not exist.");

            return CreatedAtAction("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != productDto.Id)
                return BadRequest(ModelState);

            try
            {
                await _productService.UpdateProductAsync(productDto);
                return Ok(productDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productService.IsProductExists(id))
                    return NotFound();
                else
                    return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (!await _productService.IsProductExists(id))
                return NotFound();

            var product = await _productService.DeleteProductAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProductsAsync([FromQuery] int[] ids)
        {
            foreach (var id in ids)
            {
                if (!await _productService.IsProductExists(id))
                    return NotFound();
            }

            var products = await _productService.DeleteProductsAsync(ids);
            return Ok(products);
        }
    }
}
