
namespace therabia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AccountsController(ApplicationDbContext context, IConfiguration config, IEmailService emailService)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (dto.password != dto.confirmPassword)
                return BadRequest("Passwords do not match");

            var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (userExists)
                return BadRequest("Email already registered");

            var user = new User
            {
                FullName = $"{dto.firstname} {dto.lastname}",
                Email = dto.Email,
                Password_hash = BCrypt.Net.BCrypt.HashPassword(dto.password),
                Role = Enum.Parse<UserRole>(dto.role, true),
                Is_Verified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // إنشاء Professional لو كان الدور دكتور أو مدرب أو أخصائي تغذية
            if (user.Role == UserRole.Doctor || user.Role == UserRole.Trainer || user.Role == UserRole.Nutritionist)
            {
                var professional = new Professional
                {
                    UserId = user.Id,
                    IsActive = true,
                    Bio = string.Empty,
                    Certificates = string.Empty,
                    YearsOfExperience = 0,
                    Specialization = string.Empty,
                    Price = 0,
                    AvailableTime = string.Empty,
                    Code = "", 
                    About = string.Empty,
                    Faculty = string.Empty,
                    Address = string.Empty,
                    FacebookLink = string.Empty,
                    LinkedInLink = string.Empty,
                    SubscriptionplanId = 1
                };

                _context.Professionals.Add(professional);
            }
            else if (user.Role == UserRole.Patient)
            {
                var patient = new Patient
                {
                    UserId = user.Id,
                    Height = 0,
                    Weight = 0,
                    Goals = "",
                    SubscriptionStatus = "Pending",
                    MedicalHistory = "",
                    Allergies = "",

                };

                _context.Patients.Add(patient);
            }
            else if (user.Role == UserRole.Admin)
            {
                var admin = new Admin
                {
                    UserId = user.Id
                };

                _context.Admins.Add(admin);
            }

            await _context.SaveChangesAsync();

            var token = new Verificationtoken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiryDate = DateTime.Now.AddHours(24)
            };
            _context.Verificationtokens.Add(token);
            await _context.SaveChangesAsync();

            var confirmationLink = $"https://localhost:7026/api/Accounts/ConfirmEmail?userId={user.Id}&token={token.Token}";
            await _emailService.SendEmailAsync(user.Email, "Confirm your Email", $"Click <a href='{confirmationLink}'>here</a> to confirm your email");

            return Ok("Account created successfully. Please confirm your email.");
        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(int userId, string token)
        {
            var verification = await _context.Verificationtokens
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.UserId == userId && v.Token == token);

            if (verification == null || verification.ExpiryDate < DateTime.Now)
                return BadRequest("Invalid or expired token");

            verification.User.Is_Verified = true;
            _context.Verificationtokens.Remove(verification);
            await _context.SaveChangesAsync();

            return Ok("Email confirmed successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password_hash))
                return Unauthorized("Invalid email or password");

            if (!user.Is_Verified)
                return Unauthorized("Please confirm your email first.");

            var token = JwtHelper.GenerateToken(user, _config);
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok("You have been logged out.");
        }
    }

}
