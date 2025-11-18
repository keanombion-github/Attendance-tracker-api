using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Work.Interfaces;
using System.Security.Claims;

namespace AttendanceTracker.Features.Work
{
    [ApiController]
    [Route("api/work")]
    [Authorize]
    public class TimeOutController : ControllerBase
    {
        private readonly ITimeOutCommandHandler _handler;

        public TimeOutController(ITimeOutCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("time-out")]
        public async Task<IActionResult> TimeOut()
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _handler.Handle(employeeId);
            return Ok(new { Message = "Clocked out successfully", Timestamp = DateTime.UtcNow });
        }
    }
}