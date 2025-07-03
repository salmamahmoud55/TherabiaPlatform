namespace therabia.DTO
{
    public class ProfessionalProfileDto
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specialization { get; set; }
        public decimal Price { get; set; }
        public string AvailableTime { get; set; }
        public bool Active { get; set; }

        public int TotalPatients { get; set; }
        public int TotalSessions { get; set; }
        public double AverageRate { get; set; }

        public int? NumberOfDaysPerWeek { get; set; }
        public List<string> Days { get; set; } = new();
        public List<string> Sessions { get; set; } = new();

        public string? Code { get; set; }
        public bool Disabled { get; set; }
        public int? Percent { get; set; }

        
    }

}
