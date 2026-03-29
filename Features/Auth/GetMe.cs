using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AttendanceTracker.Data;

namespace AttendanceTracker.Features.Auth
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class GetMeController : ControllerBase
    {
        private readonly Database _database;

        public GetMeController(Database database)
        {
            _database = database;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var isAdmin = User.FindFirstValue(ClaimTypes.Role) == "Admin";

            using var connection = _database.CreateConnection();
            var todayRecords = await connection.QueryAsync(
                @"SELECT TimeIn, TimeOut
                  FROM TimeRecords
                  WHERE EmployeeId = @EmployeeId
                  AND DATE(TimeIn AT TIME ZONE 'UTC') = CURRENT_DATE
                  ORDER BY TimeIn DESC",
                new { EmployeeId = id });
            var recordList = await connection.QueryAsync(
                @"SELECT TimeIn, TimeOut
                  FROM TimeRecords
                  WHERE EmployeeId = @EmployeeId
                  ORDER BY TimeIn DESC",
                new { EmployeeId = id });

            return Ok(new
            {
                Id = id,
                Email = email,
                IsAdmin = isAdmin,
                TodayRecords = todayRecords,
                Records = recordList
            });
        }
    }
}
