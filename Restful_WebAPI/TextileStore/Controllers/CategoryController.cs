using Microsoft.AspNetCore.Mvc;
using TextileStore.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TextileStore.DTO;
using TextileStore.DBContext;
using Microsoft.EntityFrameworkCore;

namespace TextileStore.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public CategoryController(ICategoryService categoryService, ApplicationDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoriesById(int categoryId)
        {
            //var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            var category = await _context.Categories.Include(c => c.Products)
                                        .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            //var categories = await _categoryService.GetAllCategoriesAsync();
            var categories = await _context.Categories.Include(c => c.Products).ToListAsync();
            return Ok(categories);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(categoryId);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO categoryDto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoriesById), new { categoryId = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, EditCategoryDTO categoryDto)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(categoryId,  categoryDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }
    }
}
