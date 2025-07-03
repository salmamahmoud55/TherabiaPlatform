namespace therabia.DTO
{
    public class MessageDto
    {
        public int ProfessionalId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
