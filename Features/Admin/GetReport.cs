using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Admin.Interfaces;

namespace AttendanceTracker.Features.Admin
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class GetReportController : ControllerBase
    {
        private readonly IGetReportQueryHandler _handler;

        public GetReportController(IGetReportQueryHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReport()
        {
            var report = await _handler.Handle();
            return Ok(report);
        }
    }
}