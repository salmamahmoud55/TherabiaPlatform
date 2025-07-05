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
    }
}
