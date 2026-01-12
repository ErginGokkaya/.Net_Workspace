namespace HepsiApi.Persistence.Configurations
{
    using Core.HepsiApi.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Bogus;

    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            Faker f = new("tr");
            var details = new List<Detail>();
            for (int i = 1; i <= 50; i++)
            {
                details.Add(new Detail
                {
                    Id = i,
                    Title = f.Lorem.Sentence(5, 3),
                    Description = f.Lorem.Paragraphs(1),
                    CategoryId = f.Random.Int(1, 4),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = i % 10 == 0 ? true : false
                });
            }
            builder.HasData(details);
        }
    }    
}