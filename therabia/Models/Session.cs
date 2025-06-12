namespace therabia.Models
{
    public enum SessionType
    {
        Call,
        VideoCall
    }
    public class Session
    {
        [Key]
        public int SessioId { get; set; }       
        public SessionType SessionType { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
        
        public int PatientId { get; set; }
        public Patient Patient { get; set; }



    }
}
