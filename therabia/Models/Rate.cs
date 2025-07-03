namespace therabia.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public int Value { get; set; } // التقييم نفسه
    }
}
