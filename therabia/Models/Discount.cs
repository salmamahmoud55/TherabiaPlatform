namespace therabia.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public int Percent { get; set; }

        public bool Disable { get; set; }

        public int ProfessionalId { get; set; }

        public Professional Professional { get; set; }

        
    }
}
