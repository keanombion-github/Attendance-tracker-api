using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Work.Interfaces;

namespace AttendanceTracker.Features.Work.Handlers
{
    public class TimeInCommandHandler : ITimeInCommandHandler
    {
        private readonly Database _database;

        public TimeInCommandHandler(Database database)
        {
            _database = database;
        }

        public async Task<DateTime> Handle(int employeeId)
        {
            using var connection = _database.CreateConnection();
            var timeIn = DateTime.UtcNow;
            await connection.ExecuteAsync(
                "INSERT INTO TimeRecords (EmployeeId, TimeIn) VALUES (@EmployeeId, @TimeIn)",
                new { EmployeeId = employeeId, TimeIn = timeIn });
            return timeIn;
        }
    }
}
