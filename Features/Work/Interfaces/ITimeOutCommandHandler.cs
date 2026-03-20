namespace AttendanceTracker.Features.Work.Interfaces
{
    public interface ITimeOutCommandHandler
    {
        Task<DateTime> Handle(int employeeId);
    }
}
