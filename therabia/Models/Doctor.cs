namespace therabia.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Bio { get; set; } = string.Empty;
        public int Experience { get; set; } = 0;
        public string Specialty { get; set; } = string.Empty;
        public string Certificates { get; set; } = string.Empty;
        

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Patient> Patients { get; set; }
        
        public int SubscriptionplanId { get; set; }
        public Subscriptionplan Subscriptionplan { get; set; }

    }
}
