namespace therabia.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool Anonymous { get; set; }
        public DateTime CreatedAt { get; set; }

        public int Value { get; set; } // التقييم نفسه

        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
