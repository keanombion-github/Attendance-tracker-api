namespace AttendanceTracker.Features.Admin
{
    public record ReportItem(int EmployeeId, string FirstName, string LastName, DateTime TimeIn, DateTime? TimeOut);
}
