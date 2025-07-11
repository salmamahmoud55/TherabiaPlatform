namespace therabia.DTO
{
    public class WalletRequestDTO
    {
        public int SessionId { get; set; }
        public decimal Cost { get; set; }
        public string? DiscountCode { get; set; }
        public string TransactionImage { get; set; }
    }
}
