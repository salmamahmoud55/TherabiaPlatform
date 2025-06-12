namespace therabia.Models
{
    public class Patientreport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string MealDescription { get; set; }
        public string ExerciseDescription { get; set; }
        public int SleepHours { get; set; }
        public int WaterIntake { get; set; }
        public string Notes { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        
    }
}
