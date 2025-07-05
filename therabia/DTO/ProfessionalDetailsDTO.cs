namespace therabia.DTO
{
    public class ProfessionalDetailsDTO
    {
        public string ProfileImage { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specialty { get; set; }
        public string Faculty { get; set; }
        public string Certificates { get; set; }
        public decimal Price { get; set; }

        public int TotalPatients { get; set; }
        public int TotalSessions { get; set; }
        public double AverageRating { get; set; }

        public List<AvailableTimeDto> AvailableTimes { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }

   

    public class CommentDTO
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public bool Anonymous { get; set; }
    }
}
