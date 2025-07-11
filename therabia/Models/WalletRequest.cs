namespace therabia.Models
{
    public class WalletRequest
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public decimal Cost { get; set; }
        public string? DiscountCode { get; set; }

        public string TransactionImage { get; set; }
    }
}
