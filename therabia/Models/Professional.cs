namespace therabia.Models
{
    public class Professional
    {
        public int Id { get; set; }
        public string Bio { get; set; }

        public string Certificates { get; set; }

        public int YearsOfExperience { get; set; }
        public string Specialization { get; set; }
        public decimal Price { get; set; }
        public string? AvailableTime { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public Discount? Discount { get; set; }
        public string? About { get; set; }
        public string? Faculty { get; set; }
       
        public string? Address { get; set; }
        public string? FacebookLink { get; set; }
        public string? LinkedInLink { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }

        public int SubscriptionplanId { get; set; }
        public Subscriptionplan Subscriptionplan { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();

        public List<AvailableTime> AvailableTimes { get; set; } = new();

        public List<ProfessionalPatient> Patients { get; set; } = new();

        public List<Rate> Rates { get; set; } = new();

        public List<Message> Messages { get; set; } = new();

        public ICollection<SubscriptionChangeRequest> SubscriptionChangeRequests { get; set; }

        public ICollection<Professionalrequest> Professionalrequests { get; set; }




    }
}
