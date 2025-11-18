namespace AttendanceTracker.Features.Admin.Interfaces
{
    public interface IGetReportQueryHandler
    {
        Task<Report> Handle();
    }
}
