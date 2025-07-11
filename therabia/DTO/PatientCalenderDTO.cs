namespace therabia.DTO
{
    public class PatientCalenderDTO
    {
        public DateTime Date { get; set; }
        public string Exercise { get; set; }
        public TimeSpan Duration { get; set; }
        public string Meals { get; set; }
        public double Water { get; set; }
        public string Notes { get; set; }
        public int PatientId { get; set; }
        public int ProfessionalId { get; set; }


    }
}
