namespace therabia.DTO
{
    public class PatientDashboardDTO
    {
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public List<ProfessionalMiniDTO> Professionals { get; set; }
        public int SessionsPerWeek { get; set; }
        public int SessionsPerMonth { get; set; }
        public List<MessageDTO> Messages { get; set; }
    }

    public class ProfessionalMiniDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class MessageDTO
    {
        public string Content { get; set; }
        public string SenderName { get; set; }

    }
}
