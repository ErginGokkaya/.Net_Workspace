using HepsiApi.Domain.Common;

namespace HepsiApi.Domain.Entities
{
    public class Brand : EntityBase
    {
        public Brand()
        {
        }

        public Brand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}