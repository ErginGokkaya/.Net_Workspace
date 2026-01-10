using System.ComponentModel.DataAnnotations;
using TextileStore.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextileStore.Entity 
{
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}