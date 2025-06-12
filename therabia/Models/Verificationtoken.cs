namespace therabia.Models
{
    public class Verificationtoken
    {
        public int Id { get; set; }
        public string token { get; set; }
        public DateTime expiry_date { get; set; }
        public bool is_used { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
