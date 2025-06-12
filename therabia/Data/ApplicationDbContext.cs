
using therabia.Models;

namespace therabia.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Trainer)
               .WithOne(d => d.User)
               .HasForeignKey<Trainer>(d => d.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Patient)
               .WithOne(d => d.User)
               .HasForeignKey<Patient>(d => d.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Nutritionist)
               .WithOne(d => d.User)
               .HasForeignKey<Nutritionist>(d => d.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Admin)
               .WithOne(d => d.User)
               .HasForeignKey<Admin>(d => d.UserId);

            modelBuilder.Entity<Message>()
              .HasOne(m => m.User)
              .WithMany(u => u.Messages)
              .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Verificationtoken>()
              .HasOne(m => m.User)
              .WithMany(u => u.Verificationtokens)
              .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Doctor>()
              .HasOne(m => m.Subscriptionplan)
              .WithMany(u => u.Doctors)
              .HasForeignKey(m => m.SubscriptionplanId);

            modelBuilder.Entity<Trainer>()
              .HasOne(m => m.Subscriptionplan)
              .WithMany(u => u.Trainers)
              .HasForeignKey(m => m.SubscriptionplanId);

            modelBuilder.Entity<Nutritionist>()
              .HasOne(m => m.Subscriptionplan)
              .WithMany(u => u.Nutritionists)
              .HasForeignKey(m => m.SubscriptionplanId);


            modelBuilder.Entity<Payment>()
             .HasOne(m => m.User)
             .WithMany(u => u.Payments)
             .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Payment>()
             .HasOne(m => m.subscriptionplan)
             .WithMany(u => u.Payments)
             .HasForeignKey(m => m.SubscriptionplanId);


            modelBuilder.Entity<Professionalrequest>()
             .HasOne(m => m.User)
             .WithMany(u => u.Professionalrequests)
             .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Professionalrequest>()
             .HasOne(m => m.Patient)
             .WithMany(u => u.Professionalrequests)
             .HasForeignKey(m => m.PatientId);

            modelBuilder.Entity<Patientreport>()
             .HasOne(m => m.Patient)
             .WithMany(u => u.Patientreports)
             .HasForeignKey(m => m.PatientId);

            modelBuilder.Entity<Session>()
             .HasOne(m => m.Patient)
             .WithMany(u => u.Sessions)
             .HasForeignKey(m => m.PatientId);

            modelBuilder.Entity<Session>()
             .HasOne(m => m.User)
             .WithMany(u => u.Sessions)
             .HasForeignKey(m => m.UserId);

            // Doctor <-> Patient
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Patients)
                .WithMany(p => p.Doctors)
                .UsingEntity<Dictionary<string, object>>(
                    "DoctorPatient",
                    j => j
                        .HasOne<Patient>()
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Doctor>()
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict));

            // Trainer <-> Patient
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.Patients)
                .WithMany(p => p.Trainers)
                .UsingEntity<Dictionary<string, object>>(
                    "TrainerPatient",
                    j => j
                        .HasOne<Patient>()
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Trainer>()
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict));

            // Nutrition <-> Patient
            modelBuilder.Entity<Nutritionist>()
                .HasMany(n => n.Patients)
                .WithMany(p => p.Nutritionists)
                .UsingEntity<Dictionary<string, object>>(
                    "NutritionPatient",
                    j => j
                        .HasOne<Patient>()
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Nutritionist>()
                        .WithMany()
                        .HasForeignKey("NutritionId")
                        .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<Professionalrequest>()
        .HasOne(pr => pr.User)
        .WithMany(u => u.Professionalrequests)
        .HasForeignKey(pr => pr.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
    .HasOne(s => s.User)
    .WithMany(u => u.Sessions)
    .HasForeignKey(s => s.UserId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
    .HasOne(s => s.Patient)
    .WithMany(p => p.Sessions)
    .HasForeignKey(s => s.PatientId)
    .OnDelete(DeleteBehavior.Restrict);


        }


        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Nutritionist> Nutritionists { get; set; }
        public DbSet<Patient> Patients { get; set; }       
        public DbSet<Patientreport> Patientreports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Verificationtoken> Verificationtokens { get; set; }
        public DbSet<Professionalrequest> Professionalrequests { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Subscriptionplan> subscriptionplans { get; set; }



    }
}
