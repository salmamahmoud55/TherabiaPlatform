namespace therabia.DTO
{
    public class EditProfileDto
    {
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? City { get; set; }
        public int YearOfExperiences { get; set; }
        public string? Specialized { get; set; }
        public string? Faculty { get; set; }
        public string? Certificates { get; set; }
        public string? Address { get; set; }
        public string? FacebookLink { get; set; }
        public string? LinkedInLink { get; set; }
    }
}
