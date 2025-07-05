using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace therabia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomeProfessionals()
        {
            var therapists = await _context.Professionals
                .Where(p => p.User.Role == UserRole.Doctor)
                .Include(p => p.User)
                .Take(5)
                .Select(p => new HomeProfessionalDTO
                {
                    Name = p.User.FullName,
                    Image = p.User.ProfileImage,
                    About = p.About,
                    FacebookLink = p.FacebookLink,
                    LinkedInLink = p.LinkedInLink
                })
                .ToListAsync();

            var healthCare = await _context.Professionals
                .Where(p => p.User.Role == UserRole.Trainer || p.User.Role == UserRole.Nutritionist)
                .Include(p => p.User)
                .Take(5)
                .Select(p => new HomeProfessionalDTO
                {
                    Name = p.User.FullName,
                    Image = p.User.ProfileImage,
                    About = p.About,
                    FacebookLink = p.FacebookLink,
                    LinkedInLink = p.LinkedInLink
                })
                .ToListAsync();

            return Ok(new
            {
                Therapists = therapists,
                HealthCare = healthCare
            });
        }


        [HttpPost("ContactUs")]
        public async Task<IActionResult> SendMessage([FromBody] ContactDTO dto)
        {
            var message = new ContactMessage
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Message = dto.Message
            };

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Thanks for reaching out!" });
        }

    }
}
