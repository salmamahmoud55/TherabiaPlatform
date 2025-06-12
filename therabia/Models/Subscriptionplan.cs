namespace therabia.Models
{
    public enum SubscriptionplanTitle
    {
        Free,
        Silver,
        Gold,
        Bronze
    }
    public class Subscriptionplan
    {
        public int Id { get; set; }
        public SubscriptionplanTitle Title { get; set; }
        public decimal Price { get; set; }
        public int MaxPatients { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
        public ICollection<Nutritionist> Nutritionists { get; set; }


        public ICollection<Payment> Payments { get; set; }

    }
}
