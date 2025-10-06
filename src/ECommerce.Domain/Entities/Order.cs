

using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Paid, Shipped, Delivered
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Relationships
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}