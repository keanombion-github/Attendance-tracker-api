using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Auth.Interfaces;

namespace AttendanceTracker.Features.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginCommandHandler _handler;

        public LoginController(ILoginCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _handler.Handle(request);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }
    }
}