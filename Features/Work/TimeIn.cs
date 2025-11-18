using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Work.Interfaces;
using System.Security.Claims;

namespace AttendanceTracker.Features.Work
{
    [ApiController]
    [Route("api/work")]
    [Authorize]
    public class TimeInController : ControllerBase
    {
        private readonly ITimeInCommandHandler _handler;

        public TimeInController(ITimeInCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("time-in")]
        public async Task<IActionResult> TimeIn()
        {
            var employeeId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _handler.Handle(employeeId);
            return Ok(new { Message = "Clocked in successfully", Timestamp = DateTime.UtcNow });
        }
    }
}