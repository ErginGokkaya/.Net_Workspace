using System.ComponentModel.DataAnnotations;
using TextileStore.Entity.Base;

namespace TextileStore.Entity
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}