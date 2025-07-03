namespace therabia.Models
{
    public class AvailableTime
    {
        public int Id { get; set; }

        public string Day { get; set; }

        public TimeSpan? Time { get; set; } 

        public int ProfessionalId { get; set; }  

        public Professional Professional { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }


    }
}
