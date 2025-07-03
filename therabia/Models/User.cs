
namespace therabia.Models
{
    public enum UserRole
    {
        Admin,
        Doctor,
        Trainer,
        Nutritionist,
        Patient
    }
    public class User
    {
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public string FullName { get; set; }
        public string? UserName { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string Email { get; set; }
        public string? Password_hash { get; set; }
        public int? Age { get; set; } = 0;
        public int? Phone { get; set; } 
        public bool Is_Verified { get; set; }
        
        public UserRole Role { get; set; }


        public Professional Profissional { get; set; }
        public Patient Patient { get; set; }
        public Admin Admin { get; set; }
        

        public ICollection<Message> Messages { get; set; }
        public ICollection<Verificationtoken> Verificationtokens { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Professionalrequest> Professionalrequests { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
