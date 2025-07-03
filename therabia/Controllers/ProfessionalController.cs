
using therabia.DTO;

namespace therabia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessionalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessionalById(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.User)
                .Include(p => p.Patients)
                .Include(p => p.Sessions)
                .Include(p => p.AvailableTimes)
                .Include(p => p.Discount)
                .Include(p => p.WorkingDays) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
                return NotFound();

            var rateValues = await _context.Rates
                .Where(r => r.ProfessionalId == professional.Id)
                .Select(r => r.Value)
                .ToListAsync();

            var dto = new ProfessionalProfileDto
            {
                Image = professional.User?.ProfileImage ?? "",
                Name = professional.User?.FullName ?? "",
                City = professional.User?.City ?? "",
                YearsOfExperience = professional.YearsOfExperience,
                Specialization = professional.Specialization,
                Price = professional.Price,
                Active = professional.IsActive,

                TotalPatients = professional.Patients?.Count ?? 0,
                TotalSessions = professional.Sessions?.Count ?? 0,
                AverageRate = ProfessionalHelper.CalculateAverageRate(rateValues),

                NumberOfDaysPerWeek = professional.WorkingDays?.Count ?? 0,
                Days = professional.WorkingDays?.Select(w => w.Day).ToList() ?? new List<string>(),
                Sessions = professional.Sessions?.Select(s => s.Type.ToString()).ToList() ?? new List<string>(),

                Code = professional.Discount?.Code,
                Percent = professional.Discount?.Percent ?? 0,
                Disabled = professional.Discount?.Disable ?? false
            };

            return Ok(dto);
        }





        [HttpPost("{id}/my_price")]
        public async Task<IActionResult> SetSessionPrice(int id, [FromBody] SessionInputDto dto)
        {
            var professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
                return NotFound("Professional not found.");
            var existingSession = await _context.Sessions
        .FirstOrDefaultAsync(s => s.ProfessionalId == id && s.Type == dto.Type);

            if (existingSession != null)
            {
                existingSession.Price = dto.Price;
                existingSession.Minutes = dto.Minutes;

                
            }
            else
            {

                var session = new Session
                {
                    ProfessionalId = id,
                    Type = dto.Type,
                    Price = dto.Price,
                    Minutes = dto.Minutes
                };

                await _context.Sessions.AddAsync(session);
            }


            await _context.SaveChangesAsync();
            return Ok("Session added successfully.");
        }


        [HttpPost("{id}/my_discount")]
        public async Task<IActionResult> SetDiscount(int id, [FromBody] DiscountDto dto)
        {
            var professional = await _context.Professionals
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
                return NotFound("Professional not found.");

            if (professional.Discount != null)
            {
                // تعديل الخصم القديم
                professional.Discount.Code = dto.Code;
                professional.Discount.Percent = dto.Percent;
                professional.Discount.Disable = dto.Disable;
            }
            else
            {
                // إضافة خصم جديد
                var discount = new Discount
                {
                    ProfessionalId = id,
                    Code = dto.Code,
                    Percent = dto.Percent,
                    Disable = dto.Disable
                };

                await _context.Discounts.AddAsync(discount);
            }

            await _context.SaveChangesAsync();
            return Ok("Discount saved successfully.");
        }



        [HttpPost("{id}/my_available_time")]
        public async Task<IActionResult> SetAvailableTimes(int id, [FromBody] AvailableTimeDto dto)
        {
            var professional = await _context.Professionals
                .Include(p => p.AvailableTimes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
                return NotFound("Professional not found.");

            // نحذف القديم
            _context.AvailableTimes.RemoveRange(professional.AvailableTimes);

            foreach (var entry in dto.Days)
            {
                var sessionExists = await _context.Sessions.AnyAsync(s => s.SessioId == entry.SessionId);
                if (!sessionExists)
                    return BadRequest($"Invalid session ID: {entry.SessionId}");

                var availableTime = new AvailableTime
                {
                    ProfessionalId = id,
                    Day = entry.Day,
                    Time = entry.Time,
                    SessionId = entry.SessionId
                };

                await _context.AvailableTimes.AddAsync(availableTime);
            }

            await _context.SaveChangesAsync();

            return Ok("Available times updated successfully.");
        }



        [HttpGet("{id}/my_patients")]
        public async Task<IActionResult> GetTrackedPatients(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.Patients)
                    .ThenInclude(pp => pp.Patient)
                .ThenInclude(p => p.User) // لو الصورة محفوظة في User
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
                return NotFound("Professional not found.");

            var result = professional.Patients.Select(pp => new TrackedPatientDto
            {
                Id = pp.PatientId,
                Image = pp.Patient.User?.ProfileImage ?? "",
                Exercise = pp.Exercise,
                Duration = pp.Duration,
                Meals = pp.Meals,
                Water = pp.Water,
                Notes = pp.Notes
            }).ToList();

            return Ok(result);
        }


        [HttpPost("{id}/send_message")]
        public async Task<IActionResult> SendMessageToPatient([FromBody] MessageDto dto)
        {
            // تأكد إن المريض والدكتور موجودين
            var professional = await _context.Professionals.FindAsync(dto.ProfessionalId);
            var patient = await _context.Patients.FindAsync(dto.PatientId);

            if (professional == null || patient == null)
                return NotFound("Professional or Patient not found.");

            var message = new Message
            {
                ProfessionalId = dto.ProfessionalId,
                PatientId = dto.PatientId,
                Name = dto.Name,
                Content = dto.Content,
                UserId = professional.UserId
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return Ok("Message sent successfully.");
        }


        [HttpGet("{id}/my_wallet")]
        public async Task<IActionResult> GetWallet(int id)
        {
            var professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
                return NotFound("Professional not found.");

            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);

            var sessions = await _context.Sessions
                .Include(s => s.Patient)
                    .ThenInclude(p => p.User)
                .Where(s => s.ProfessionalId == id &&
                            s.SessionDate >= startOfMonth &&
                            s.SessionDate < startOfNextMonth)
                .ToListAsync();

            var dto = new WalletDto
            {
                Month = now.ToString("MMMM"),
                TotalEarning = sessions.Sum(s => s.Price),
                Sessions = sessions.Select(s => new myWalletDto
                {
                    PatientId = s.PatientId,
                    PatientName = s.Patient.User?.FullName ?? "",
                    PatientImage = s.Patient.User?.ProfileImage ?? "",
                    SessionId = s.SessioId,
                    Price = s.Price,
                    Type = s.Type,
                    Minutes = s.Minutes
                }).ToList()
            };

            return Ok(dto);
        }



        [HttpPut("{id}/edit_profile")]
        public async Task<IActionResult> EditProfile(int id, [FromBody] EditProfileDto dto)
        {
            var professional = await _context.Professionals
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null || professional.User == null)
                return NotFound("Professional or User not found.");

            professional.User.ProfileImage = dto.ImageUrl;
            professional.About = dto.About;
            professional.User.FullName = dto.Name;
            professional.User.Gender = dto.Gender;
            professional.User.City = dto.City;
            professional.YearsOfExperience = dto.YearOfExperiences;
            professional.Specialization = dto.Specialized;
            professional.Faculty = dto.Faculty;
            professional.Certificates = dto.Certificates;
            professional.Address = dto.Address;
            professional.FacebookLink = dto.FacebookLink;
            professional.LinkedInLink = dto.LinkedInLink;

            await _context.SaveChangesAsync();

            return Ok("Profile updated successfully.");
        }




        [HttpGet("{id}/edit_profile")]
        public async Task<IActionResult> GetEditProfile(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null || professional.User == null)
                return NotFound("Professional or User not found.");

            var dto = new EditProfileDto
            {
                ImageUrl = professional.User.ProfileImage,
                About = professional.About,
                Name = professional.User.FullName,
                Gender = professional.User.Gender,
                City = professional.User.City,
                YearOfExperiences = professional.YearsOfExperience,
                Specialized = professional.Specialization,
                Faculty = professional.Faculty,
                Certificates = professional.Certificates,
                Address = professional.Address,
                FacebookLink = professional.FacebookLink,
                LinkedInLink = professional.LinkedInLink
            };

            return Ok(dto);
        }



        [HttpGet("{id}/my_planes")]
        public async Task<IActionResult> GetMyPlans(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.Subscriptionplan)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
                return NotFound();

            var availablePlans = await _context.subscriptionplans
                .Select(p => new PlanDto
                {
                    Id = p.Id,
                    Type = p.Type,
                    MaxPatients = p.MaxPatients,
                    Price = p.Price
                }).ToListAsync();

            var response = new MyPlansResponseDto
            {
                CurrentPlanType = professional.Subscriptionplan?.Type ?? PlanType.Free,
                CurrentMaxPatients = professional.Subscriptionplan?.MaxPatients ?? 0,
                CurrentPrice = professional.Subscriptionplan?.Price ?? 0,
                AvailablePlans = availablePlans
            };

            return Ok(response);
        }

        [HttpPost("admin/my_request")]
        public async Task<IActionResult> RequestPlanChange([FromBody] SubscriptionChangeRequestDto dto)
        {
            var professional = await _context.Professionals.FindAsync(dto.ProfessionalId);
            var newPlan = await _context.subscriptionplans.FindAsync(dto.SubscriptionPlanId);

            if (professional == null || newPlan == null)
                return NotFound("Professional or Plan not found.");

            var existingRequest = await _context.SubscriptionChangeRequests
                .FirstOrDefaultAsync(r => r.ProfessionalId == dto.ProfessionalId &&
                                          r.SubscriptionPlanId == dto.SubscriptionPlanId &&
                                          !r.IsApproved);
            if (existingRequest != null)
            {
                return BadRequest("You already have a pending request for this plan.");
            }

            var request = new SubscriptionChangeRequest
            {
                ProfessionalId = dto.ProfessionalId,
                SubscriptionPlanId = dto.SubscriptionPlanId,
                TransactionImage = dto.TransactionImage,
                RequestDate = DateTime.UtcNow,
                IsApproved = false
            };

            await _context.SubscriptionChangeRequests.AddAsync(request);
            await _context.SaveChangesAsync();

            return Ok("Subscription change request submitted successfully.");
        }






    }
}
