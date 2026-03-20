namespace AttendanceTracker.Features.Admin
{
    public record ReportItem(
        int EmployeeId,
        string FirstName,
        string LastName,
        DateTime TimeIn,
        DateTime? TimeOut)
    {
        public string Date => TimeIn.ToLocalTime().ToString("yyyy-MM-dd");
        public string TimeInFormatted => TimeIn.ToLocalTime().ToString("hh:mm tt");
        public string? TimeOutFormatted => TimeOut?.ToLocalTime().ToString("hh:mm tt");
        public string? Duration => TimeOut.HasValue
            ? $"{(TimeOut.Value - TimeIn).Hours}h {(TimeOut.Value - TimeIn).Minutes}m"
            : "Still clocked in";
    }
}
