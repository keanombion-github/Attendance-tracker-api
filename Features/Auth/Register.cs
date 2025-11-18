using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Auth.Interfaces;

namespace AttendanceTracker.Features.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterCommandHandler _handler;

        public RegisterController(IRegisterCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var (success, message, employeeId) = await _handler.Handle(request);
            if (!success)
                return BadRequest(new { Message = message });
            return Ok(new { Message = message, EmployeeId = employeeId });
        }
    }
}
