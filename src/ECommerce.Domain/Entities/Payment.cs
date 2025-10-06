
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Card"; // Card, PayPal, etc.
        public string Status { get; set; } = "Pending"; // Pending, Successful, Failed
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? TransactionId { get; set; }
    }
}