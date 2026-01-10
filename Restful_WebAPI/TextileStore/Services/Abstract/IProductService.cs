namespace TextileStore.Services.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextileStore.DTO;
    using TextileStore.Entity;

    public interface IProductService
    {
        Task<CreateProductDTO> CreateProductAsync(CreateProductDTO productDto);
        Task<EditProductDTO> UpdateProductAsync(int categoryId, EditProductDTO productDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
    }
}