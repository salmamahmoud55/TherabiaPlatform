namespace therabia.DTO
{
    public class AvailableTimeDto
    {
        public List<DayInput> Days { get; set; } = new();
    }

    public class DayInput
    {
        public string Day { get; set; } = string.Empty;
        public TimeSpan? Time { get; set; } 
        public int SessionId { get; set; }
    }
}
