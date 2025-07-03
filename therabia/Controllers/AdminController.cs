
namespace therabia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpGet("{id}/patient_requests")]
        public async Task<IActionResult> GetAllPatientRequests(int id)
        {
            var requests = await _context.Professionalrequests
                .Include(r => r.Patient)
                    .ThenInclude(p => p.User)
                .Include(r => r.Professional)
                    .ThenInclude(p => p.User)
                .Where(r => r.Status == null) // الطلبات التي لم يتم الرد عليها بعد
                .Select(r => new
                {
                    RequestId = r.Id,
                    RequestDate = r.CreatedAt,
                    Price = r.Price,
                    TransactionImage = r.TransactionImage,
                    PatientName = r.Patient.User.FullName,
                    ProfessionalName = r.Professional.User.FullName
                })
                .ToListAsync();

            return Ok(requests);
        }



        [HttpGet("{id}/professional_requests")]
        public async Task<IActionResult> GetAllProfessionalRequests(int id)
        {
            var requests = await _context.SubscriptionChangeRequests
                .Include(r => r.Professional)
                    .ThenInclude(p => p.User)
                .Include(r => r.SubscriptionPlan)
                .Where(r => r.Status == RequestStatus.Pending) // الطلبات المعلقة فقط
                .Select(r => new
                {
                    RequestId = r.Id,
                    RequestDate = r.RequestDate,
                    TransactionImage = r.TransactionImage,
                    ProfessionalName = r.Professional.User.FullName,
                    PlanType = r.SubscriptionPlan.Type.ToString(),
                    Price = r.SubscriptionPlan.Price
                })
                .ToListAsync();

            return Ok(requests);
        }




        [HttpPost("patient_requests/{requestId}/approve")]
        public async Task<IActionResult> ApprovePatientRequest(int requestId)
        {
            var request = await _context.Professionalrequests.FindAsync(requestId);
            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Accepted;
            await _context.SaveChangesAsync();
            return Ok("Patient request approved.");
        }




        [HttpPost("patient_requests/{requestId}/reject")]
        public async Task<IActionResult> RejectPatientRequest(int requestId)
        {
            var request = await _context.Professionalrequests.FindAsync(requestId);
            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();
            return Ok("Patient request rejected.");
        }



        [HttpPost("professional_requests/{requestId}/approve")]
        public async Task<IActionResult> ApprovePlanChangeRequest(int requestId)
        {
            var request = await _context.SubscriptionChangeRequests
                .Include(r => r.Professional)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Accepted;
            request.Professional.SubscriptionplanId = request.SubscriptionPlanId;

            await _context.SaveChangesAsync();
            return Ok("Plan change request approved.");
        }




        [HttpPost("professional_requests/{requestId}/reject")]
        public async Task<IActionResult> RejectPlanChangeRequest(int requestId)
        {
            var request = await _context.SubscriptionChangeRequests.FindAsync(requestId);
            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();
            return Ok("Plan change request rejected.");
        }


    }
}
