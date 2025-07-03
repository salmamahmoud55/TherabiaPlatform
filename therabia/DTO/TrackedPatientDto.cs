namespace therabia.DTO
{
    public class TrackedPatientDto
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string? Exercise { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Meals { get; set; }
        public double? Water { get; set; }
        public string? Notes { get; set; }
    }
}
