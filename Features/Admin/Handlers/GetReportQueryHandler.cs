using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Admin.Interfaces;

namespace AttendanceTracker.Features.Admin.Handlers
{
    public class GetReportQueryHandler : IGetReportQueryHandler
    {
        private readonly Database _database;

        public GetReportQueryHandler(Database database)
        {
            _database = database;
        }

        public async Task<Report> Handle()
        {
            using var connection = _database.CreateConnection();
            var items = await connection.QueryAsync<ReportItem>(
                @"SELECT e.Id AS EmployeeId, e.FirstName, e.LastName, tr.TimeIn, tr.TimeOut
                  FROM Employees e
                  JOIN TimeRecords tr ON e.Id = tr.EmployeeId
                  ORDER BY e.Id, tr.TimeIn");
            return new Report(items);
        }
    }
}
