namespace therabia.DTO
{
    public class PatientRequestDto
    {
        public int RequestId { get; set; }
        public string PatientName { get; set; }
        public string ProfessionalName { get; set; }
        public DateTime SessionDate { get; set; }
        public decimal Price { get; set; }
        public string TransactionImage { get; set; }
        public string Status { get; set; }
    }
}
