using HepsiApi.Domain.Common;
using HepsiApi.Domain.Entities;
using System.Collections.Generic;

namespace HepsiApi.Domain.Entities
{
    public class Product : EntityBase
    {
        public Product()
        {
        }
        public Product(string title, string description, int brandId, decimal price, decimal discounted)
        {
            Title = title;
            Description = description;
            BrandId = brandId;
            Price = price;
            Discounted = discounted;
        }

        public required string Title { get; set; }
        public required string Description { get; set; }
        //public required string ImagePath { get; set; }
        public required int BrandId { get; set; }
        public Brand Brand { get; set; }
        public required decimal Price { get; set; }
        public required decimal Discounted { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}