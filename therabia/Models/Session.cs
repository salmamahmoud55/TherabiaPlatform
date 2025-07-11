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
        public SessionType Type { get; set; }
        public int? Rate { get; set; }
        public int Minutes { get; set; }
        public DateTime SessionDate { get; set; }
        public double Price { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
        
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int ProfessionalId { get; set; }  
        public Professional profissional { get; set; }

        public Professionalrequest professionalrequest { get; set; }
        public WalletRequest WalletRequest { get; set; }



    }
}
