
using System.Numerics;
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
                .HasOne(u => u.Profissional)
                .WithOne(d => d.User)
                .HasForeignKey<Professional>(d => d.UserId);

            

            modelBuilder.Entity<User>()
               .HasOne(u => u.Patient)
               .WithOne(d => d.User)
               .HasForeignKey<Patient>(d => d.UserId);

            

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

            modelBuilder.Entity<Professional>()
              .HasOne(m => m.Subscriptionplan)
              .WithMany(u => u.Profissionals)
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

            modelBuilder.Entity<Professionalrequest>()
             .HasOne(m => m.Professional)
             .WithMany(u => u.Professionalrequests)
             .HasForeignKey(m => m.ProfessionalId);

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

            modelBuilder.Entity<Session>()
    .HasOne(s => s.profissional)
    .WithMany(d => d.Sessions)
    .HasForeignKey(s => s.ProfessionalId)
    .OnDelete(DeleteBehavior.Restrict);


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

            modelBuilder.Entity<WorkingDay>()
        .HasOne(w => w.Profissional)
        .WithMany(d => d.WorkingDays)
        .HasForeignKey(w => w.ProfessionalId)
        .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ProfessionalPatient>()
    .HasKey(pp => new { pp.ProfessionalId, pp.PatientId });

            modelBuilder.Entity<ProfessionalPatient>()
                .HasOne(pp => pp.Professional)
                .WithMany(p => p.Patients)
                .HasForeignKey(pp => pp.ProfessionalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProfessionalPatient>()
                .HasOne(pp => pp.Patient)
                .WithMany(p => p.Professionals)
                .HasForeignKey(pp => pp.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Professionalrequest>()
       .HasOne(pr => pr.Session)
       .WithOne(s => s.professionalrequest)
       .HasForeignKey<Professionalrequest>(pr => pr.SessionId)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<WalletRequest>()
       .HasOne(pr => pr.Session)
       .WithOne(s => s.WalletRequest)
       .HasForeignKey<WalletRequest>(pr => pr.SessionId)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<WalletRequest>()
       .HasOne(pr => pr.Patient)
       .WithOne(s => s.WalletRequest)
       .HasForeignKey<WalletRequest>(pr => pr.PatientId)
       .OnDelete(DeleteBehavior.SetNull);





            modelBuilder.Entity<Professional>()
            .HasMany(d => d.AvailableTimes)
            .WithOne(a => a.Professional)
            .HasForeignKey(a => a.ProfessionalId).OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Professional>()
                .HasOne(d => d.Discount)
                .WithOne(dis => dis.Professional)
                .HasForeignKey<Discount>(d => d.ProfessionalId).OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Rate>()
     .HasOne(r => r.Professional)
     .WithMany(p => p.Rates)
     .HasForeignKey(r => r.ProfessionalId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rate>()
     .HasOne(r => r.User)
     .WithMany(p => p.Rates)
     .HasForeignKey(r => r.UserId)
     .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Message>()
    .HasOne(m => m.Professional)
    .WithMany(p => p.Messages)
    .HasForeignKey(m => m.ProfessionalId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Messages)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<AvailableTime>()
    .HasOne(a => a.Session)
    .WithMany()
    .HasForeignKey(a => a.SessionId).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<SubscriptionChangeRequest>()
    .HasOne(r => r.Professional)
    .WithMany(p => p.SubscriptionChangeRequests)
    .HasForeignKey(r => r.ProfessionalId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubscriptionChangeRequest>()
                .HasOne(r => r.SubscriptionPlan)
                .WithMany(p => p.SubscriptionChangeRequests)
                .HasForeignKey(r => r.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
    .HasOne(m => m.Sender)
    .WithMany()
    .HasForeignKey(m => m.SenderId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);





            modelBuilder.Entity<Subscriptionplan>().HasData(
    new Subscriptionplan { Id = 1, Type = PlanType.Free, MaxPatients = 20, Price = 0 },
    new Subscriptionplan { Id = 2, Type = PlanType.Bronze, MaxPatients = 50, Price = 300 },
    new Subscriptionplan { Id = 3, Type = PlanType.Silver, MaxPatients = 150, Price = 500 },
    new Subscriptionplan { Id = 4, Type = PlanType.Gold, MaxPatients = 300, Price = 800 }
);




        }


        public DbSet<User> Users { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        public DbSet<Patient> Patients { get; set; }       
        public DbSet<Patientreport> Patientreports { get; set; }
        public DbSet<Payment> Payments { get; set; }
       
        public DbSet<Admin> Admins { get; set; }
        
        public DbSet<Professionalrequest> Professionalrequests { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Subscriptionplan> subscriptionplans { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<Verificationtoken> Verificationtokens { get; set; }

        public DbSet<ProfessionalPatient> ProfessionalPatients { get; set; }

        public DbSet<AvailableTime> AvailableTimes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<SubscriptionChangeRequest> SubscriptionChangeRequests { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<WalletRequest> WalletRequests { get; set; }







    }
}
