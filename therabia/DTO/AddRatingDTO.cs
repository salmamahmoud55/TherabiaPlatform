namespace therabia.DTO
{
    public class AddRatingDTO
    {
        public int ProfessionalId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public bool Anonymous { get; set; }

    }
}
