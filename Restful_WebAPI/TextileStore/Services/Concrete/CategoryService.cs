namespace TextileStore.Services.Concrete
{
    using TextileStore.Services.Abstract;
    using TextileStore.Entity;
    using TextileStore.DBContext;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextileStore.DTO;
    using AutoMapper;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateCategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<EditCategoryDTO> UpdateCategoryAsync(int categoryId, EditCategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return null;
            }

            category.CategoryName = categoryDto.CategoryName;
            category.Description = categoryDto.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<EditCategoryDTO>(category);
        }
    }
}