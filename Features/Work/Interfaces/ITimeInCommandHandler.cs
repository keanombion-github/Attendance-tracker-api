namespace AttendanceTracker.Features.Work.Interfaces
{
    public interface ITimeInCommandHandler
    {
        Task Handle(int employeeId);
    }
}
