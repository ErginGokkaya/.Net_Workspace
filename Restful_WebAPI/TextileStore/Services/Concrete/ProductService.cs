namespace TextileStore.Services.Concrete
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TextileStore.DTO;
    using TextileStore.Entity;
    using TextileStore.Services.Abstract;
    using TextileStore.DBContext;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<CreateProductDTO> CreateProductAsync(CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return productDto;
        }

        public async Task<EditProductDTO> UpdateProductAsync(int categoryId, EditProductDTO productDto)
        {
            var product = await _context.Products.FindAsync(categoryId);
            if (product == null)
            {
                return null;
            }
            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();
            return productDto;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}