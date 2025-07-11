using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace therabia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("professionals/{id}")]
        public async Task<IActionResult> GetProfessionalDetails(int id)
        {
            var professional = await _context.Professionals
                .Include(p => p.User)
                .Include(p => p.Sessions)
                .Include(p => p.Rates)
                .Include(p => p.AvailableTimes)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (professional == null)
                return NotFound("Professional not found");

            var dto = new ProfessionalDetailsDTO
            {
                ProfileImage = professional.User.ProfileImage,
                Name = professional.User.FullName,
                About = professional.About,
                City = professional.User.City,
                Gender = professional.User.Gender,
                YearsOfExperience = professional.YearsOfExperience,
                Specialty = professional.Specialization,
                Faculty = professional.Faculty,
                Certificates = professional.Certificates,
                Price = professional.Price,

                TotalPatients = professional.Sessions.Select(s => s.PatientId).Distinct().Count(),
                TotalSessions = professional.Sessions.Count,
                AverageRating = professional.Rates.Any() ? professional.Rates.Average(r => r.Value) : 0,

                AvailableTimes = professional.AvailableTimes.Select(a => new AvailableTimeDto
                {
                    Day = a.Day,
                    Time = a.Time
                }).ToList(),

                Comments = professional.Rates.Select(r => new CommentDTO
                {
                    UserName = r.Anonymous ? "Anonymous" : r.User.FullName,
                    Date = r.CreatedAt,
                    Comment = r.Comment,
                    Rate = r.Value,
                    Anonymous = r.Anonymous
                }).ToList()
            };

            return Ok(dto);
        }




        [Authorize(Roles = "Patient")]
        [HttpPost("professionals/rate")]
        public async Task<IActionResult> AddRating(AddRatingDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var professional = await _context.Professionals.FindAsync(dto.ProfessionalId);
            if (professional == null) return NotFound("Professional not found");

            var rating = new Rate
            {
                UserId = userId,
                ProfessionalId = dto.ProfessionalId,
                Value = dto.Rate,
                Comment = dto.Comment,
                Anonymous = dto.Anonymous,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rates.Add(rating);
            await _context.SaveChangesAsync();

            return Ok("Rating added successfully");
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDashboard(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Professionals).ThenInclude(pp => pp.Professional).ThenInclude(p => p.User)
                .Include(p => p.Sessions)
                .Include(p => p.Messages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return NotFound("Patient not found");

            var professionals = patient.Professionals.Select(pp => new
            {
                Name = pp.Professional.User.FullName,
                Image = pp.Professional.User.ProfileImage
            }).ToList();

            var now = DateTime.Now;
            var oneWeekAgo = now.AddDays(-7);
            var oneMonthAgo = now.AddMonths(-1);

            var sessionsPerWeek = patient.Sessions.Count(s => s.SessionDate >= oneWeekAgo);
            var sessionsPerMonth = patient.Sessions.Count(s => s.SessionDate >= oneMonthAgo);

            var messages = patient.Messages
                .Where(m => m.SenderId != null)
                .Select(m => new
                {
                    Content = m.Content,
                    SenderName = m.Sender.FullName
                }).ToList();

            return Ok(new
            {
                UserImage = patient.User.ProfileImage,
                UserName = patient.User.FullName,
                Professionals = professionals,
                SessionsPerWeek = sessionsPerWeek,
                SessionsPerMonth = sessionsPerMonth,
                Messages = messages
            });
        }



        [HttpPost("{id}/my_records")]
        public async Task<IActionResult> SubmitCalendarEntry([FromBody] PatientCalenderDTO dto)
        {
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            var professional = await _context.Professionals.FindAsync(dto.ProfessionalId);

            if (patient == null || professional == null)
                return BadRequest("Invalid patient or professional ID.");

            var entry = new ProfessionalPatient
            {
                PatientId = dto.PatientId,
                ProfessionalId = dto.ProfessionalId,
                Date = dto.Date,
                Exercise = dto.Exercise,
                Duration = dto.Duration,
                Meals = dto.Meals,
                Water = dto.Water,
                Notes = dto.Notes
            };

            _context.ProfessionalPatients.Add(entry);
            await _context.SaveChangesAsync();

            return Ok("Calendar entry saved successfully.");
        }


        [HttpGet("upcomingsessions")]
        public async Task<IActionResult> GetUpcomingSessions(int patientId)
        {
            var upcomingSessions = await _context.Sessions
                .Include(s => s.profissional)
                    .ThenInclude(p => p.User)
                .Where(s => s.PatientId == patientId && s.SessionDate > DateTime.Now)
                .Select(s => new SimpleSessionDto
                {
                    Time = s.SessionDate,
                    ProfessionalName = s.profissional.User.FullName
                })
                .ToListAsync();
            if (!upcomingSessions.Any())
                return Ok("you have no upcoming sessions.");

            return Ok(upcomingSessions);
        }


        [HttpGet("previoussessions")]
        public async Task<IActionResult> GetPreviousSessions(int patientId)
        {
            var previousSessions = await _context.Sessions
                .Include(s => s.profissional)
                    .ThenInclude(p => p.User)
                .Where(s => s.PatientId == patientId && s.SessionDate <= DateTime.Now)
                .Select(s => new SimpleSessionDto
                {
                    Time = s.SessionDate,
                    ProfessionalName = s.profissional.User.FullName
                })
                .ToListAsync();
            if (!previousSessions.Any())
                return Ok("you have no previous sessions.");

            return Ok(previousSessions);
        }


        [HttpGet("requests")]
        public async Task<IActionResult> GetMyRequests(int patientId)
        {
            var requests = await _context.Professionalrequests
                .Include(r => r.Professional)
                    .ThenInclude(p => p.User)
                .Where(r => r.PatientId == patientId)
                .Select(r => new MyRequestDTO
                {
                    ProfessionalName = r.Professional.User.FullName,
                    SessionDate = r.SessionDate,
                    Status = r.Status
                })
                .ToListAsync();

            if (!requests.Any())
                return Ok("You have no requests yet.");

            return Ok(requests);
        }



        [HttpPost("wallet")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateWalletRequest([FromBody] WalletRequestDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null)
                return BadRequest("Patient not found");

            var session = await _context.Sessions.FindAsync(dto.SessionId);
            if (session == null)
                return BadRequest("Session not found");

            var request = new WalletRequest
            {
                PatientId = patient.Id,
                SessionId = dto.SessionId,
                Cost = dto.Cost,
                DiscountCode = dto.DiscountCode,
                TransactionImage = dto.TransactionImage,
                
            };

            _context.WalletRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok("Wallet request sent successfully");
        }

    }
}
