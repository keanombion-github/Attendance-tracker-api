namespace AttendanceTracker.Features.Work.Interfaces
{
    public interface ITimeInCommandHandler
    {
        Task<DateTime> Handle(int employeeId);
    }
}
