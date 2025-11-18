using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Work.Interfaces;

namespace AttendanceTracker.Features.Work.Handlers
{
    public class TimeOutCommandHandler : ITimeOutCommandHandler
    {
        private readonly Database _database;

        public TimeOutCommandHandler(Database database)
        {
            _database = database;
        }

        public async Task Handle(int employeeId)
        {
            using var connection = _database.CreateConnection();
            await connection.ExecuteAsync(
                @"UPDATE TimeRecords 
                  SET TimeOut = @TimeOut 
                  WHERE Id = (
                      SELECT Id 
                      FROM TimeRecords 
                      WHERE EmployeeId = @EmployeeId AND TimeOut IS NULL 
                      ORDER BY TimeIn DESC 
                      LIMIT 1
                  )",
                new { EmployeeId = employeeId, TimeOut = DateTime.UtcNow });
        }
    }
}
