namespace therabia.Models
{
    public class ProfessionalPatient
    {
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        // بيانات المتابعة
        public string? Exercise { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Meals { get; set; }
        public double? Water { get; set; }
        public string? Notes { get; set; }
    }
}
