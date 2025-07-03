namespace therabia.Models
{
    public enum PlanType
    {
        Free,
        Silver,
        Gold,
        Bronze
    }
    public class Subscriptionplan
    {
        public int Id { get; set; }
        public PlanType Type { get; set; }
        public decimal Price { get; set; }
        public int MaxPatients { get; set; }

        public ICollection<Professional> Profissionals { get; set; }
        


        public ICollection<Payment> Payments { get; set; }

        public ICollection<SubscriptionChangeRequest> SubscriptionChangeRequests { get; set; }

    }
}
