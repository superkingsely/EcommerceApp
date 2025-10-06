using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        // Relationships
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}