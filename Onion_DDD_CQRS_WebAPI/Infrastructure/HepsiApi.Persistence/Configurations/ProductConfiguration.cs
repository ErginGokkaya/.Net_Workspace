namespace HepsiApi.Persistence.Configurations
{
    using Core.HepsiApi.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Bogus;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            Faker f = new("tr");
            var products = new List<Product>();
            for (int i = 1; i <= 100; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Title = f.Commerce.ProductName(),
                    Description = f.Commerce.ProductDescription(),
                    Discounted = f.Random.Decimal(5, 50),
                    Price = decimal.Parse(f.Commerce.Price(10, 1000)),
                    BrandId = f.Random.Int(1, 10),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = i % 15 == 0 ? true : false
                });
            }
            builder.HasData(products);
        }
    }
}    