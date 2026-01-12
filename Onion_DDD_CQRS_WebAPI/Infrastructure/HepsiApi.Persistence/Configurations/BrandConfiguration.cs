namespace HepsiApi.Persistence.Configurations
{
    using Core.HepsiApi.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Bogus;
    

    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(256);

            Faker f = new("tr");

            var brands = new List<Brand>();
            for (int i = 1; i <= 10; i++)
            {
                brands.Add(new Brand
                {
                    Id = i,
                    Name = f.Company.CompanyName(),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = i%4==0 ? true : false
                });
            }
            builder.HasData(brands);


        }
    }
}