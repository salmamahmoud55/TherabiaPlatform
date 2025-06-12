using System.ComponentModel.DataAnnotations.Schema;

namespace therabia.Models
{
    public enum PaymentStatus
    {
        Completed,
        Failed
    }
    public class Payment
    {
        public int Id { get; set; }
        
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime Timestamp { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; }

        public int SubscriptionplanId { get; set; }
        public Subscriptionplan subscriptionplan { get; set; }
    }
}
