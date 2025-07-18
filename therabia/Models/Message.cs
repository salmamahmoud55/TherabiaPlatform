﻿namespace therabia.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public string Name { get; set; } = string.Empty; // اسم الدكتور
        public string Content { get; set; } = string.Empty; // محتوى الرسالة

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }

        public int SenderId { get; set; }       // اللي بعت الرسالة (UserId)
        public User Sender { get; set; }

        public int ReceiverId { get; set; }     // اللي استلم الرسالة (UserId)
        public User Receiver { get; set; }




    }
}
