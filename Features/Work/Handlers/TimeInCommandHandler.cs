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

        public async Task Handle(int employeeId)
        {
            using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(
                "INSERT INTO TimeRecords (EmployeeId, TimeIn) VALUES (@EmployeeId, @TimeIn)",
                new { EmployeeId = employeeId, TimeIn = DateTime.UtcNow });
        }
    }
}
