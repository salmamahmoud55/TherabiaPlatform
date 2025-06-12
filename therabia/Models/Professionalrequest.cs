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


        public int UserId { get; set; }
        public User User { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
