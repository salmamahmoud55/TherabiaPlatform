namespace therabia.DTO
{
    public class MyPlansResponseDto
    {
        public PlanType CurrentPlanType { get; set; }
        public int? CurrentMaxPatients { get; set; }
        public decimal? CurrentPrice { get; set; }

        public List<PlanDto> AvailablePlans { get; set; } = new();
    }

    public class PlanDto
    {
        public int Id { get; set; }
        public PlanType Type { get; set; } 
        public int MaxPatients { get; set; }
        public decimal Price { get; set; }
    }
    
}
