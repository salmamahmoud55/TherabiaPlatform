namespace therabia.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Assigned_Trainer_Id { get; set; }
        public string Assigned_Doctor_Id { get; set; }
        public string Assigned_Nutritionist_Id { get; set; }

        [Precision(5 , 2)]
        public decimal Height { get; set; }

        [Precision(5, 2)]
        public decimal Weight { get; set; }
        public string Goals { get; set; }
        public string SubscriptionStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Trainer> Trainers { get; set; }
        public ICollection<Nutritionist> Nutritionists { get; set; }

        public ICollection<Professionalrequest> Professionalrequests { get; set; }
        public ICollection<Patientreport> Patientreports { get; set; }

        public ICollection<Session> Sessions { get; set; }


    }
}
