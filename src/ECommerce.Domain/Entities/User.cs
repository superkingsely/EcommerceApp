
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = "Customer"; // Admin, Customer, etc.

        // Relationships
        public Cart? Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}