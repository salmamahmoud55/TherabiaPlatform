namespace therabia.Models
{
    public class Nutritionist
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public int Experience { get; set; }
        public string Specialty { get; set; }
        public string Certificates { get; set; }
       

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Patient> Patients { get; set; }

        public int SubscriptionplanId { get; set; }
        public Subscriptionplan Subscriptionplan { get; set; }
    }
}
