namespace TextileStore.Services.Abstract
{
    using TextileStore.Entity;
    using TextileStore.DTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO category);
        Task<EditCategoryDTO> UpdateCategoryAsync(int categoryId, EditCategoryDTO category);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}