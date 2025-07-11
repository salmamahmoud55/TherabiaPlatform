namespace therabia.Models
{
    public enum ProfessionalType
    {
        Trainer,
        Nutritionist,
        Doctor,
        Patient
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class Professionalrequest
    {
        public int Id { get; set; }
        public ProfessionalType ProfessionalType { get; set; }
        public RequestStatus Status { get; set; }       
        public DateTime CreatedAt { get; set; }
        public string TransactionImage { get; set; }
        public bool IsApproved { get; set; }
        public double Price { get; set; }
        public DateTime SessionDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public int? SessionId { get; set; }
        public Session Session { get; set; }

    }
}
