namespace therabia.Models
{
    public class Verificationtoken
    {
        public int Id { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
