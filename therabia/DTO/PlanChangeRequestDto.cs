namespace therabia.DTO
{
    public class PlanChangeRequestDto
    {
        public int RequestId { get; set; }
        public string ProfessionalName { get; set; }
        public string OldPlan { get; set; }
        public string NewPlan { get; set; }
        public decimal Price { get; set; }
        public string TransactionImage { get; set; }
        public string Status { get; set; }
    }
}
