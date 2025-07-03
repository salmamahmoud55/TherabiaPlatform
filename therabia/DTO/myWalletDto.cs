namespace therabia.DTO
{
    public class myWalletDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientImage { get; set; } = string.Empty;

        public int SessionId { get; set; }
        public double Price { get; set; }
        public SessionType Type { get; set; } 
        public int Minutes { get; set; }
    }

    public class WalletDto
    {
        public string Month { get; set; } = string.Empty;
        public double TotalEarning { get; set; }
        public List<myWalletDto> Sessions { get; set; } = new();
    }
}
