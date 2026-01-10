namespace TextileStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TextileStore.Services.Abstract;
    using TextileStore.Entity;
    using TextileStore.DTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextileStore.DBContext;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductDTO>> CreateProduct(CreateProductDTO productDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EditProductDTO>> UpdateProduct(int id, EditProductDTO productDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return updatedProduct;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 