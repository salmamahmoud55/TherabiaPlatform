namespace therabia.Models
{
    public class WorkingDay
    {
        public int Id { get; set; }

        public string Day { get; set; } = string.Empty; 

        
        public int? ProfessionalId { get; set; }
        public Professional Profissional { get; set; }

        
    }

}
