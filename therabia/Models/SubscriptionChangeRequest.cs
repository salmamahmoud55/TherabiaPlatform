namespace therabia.Models
{

    
    public class SubscriptionChangeRequest
    {
        [Key]
        public int Id { get; set; }

        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public int SubscriptionPlanId { get; set; }
        public Subscriptionplan SubscriptionPlan { get; set; }

        public string TransactionImage { get; set; }
        public bool IsApproved { get; set; } = false;

        public RequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}
