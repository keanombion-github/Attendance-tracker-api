using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Features.Admin.Interfaces;

namespace AttendanceTracker.Features.Admin
{


    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class GetAllUsersController : ControllerBase
    {
        private readonly IGetAllUsersQueryHandler _handler;

        public GetAllUsersController(IGetAllUsersQueryHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _handler.Handle();
            return Ok(users);
        }
    }
}