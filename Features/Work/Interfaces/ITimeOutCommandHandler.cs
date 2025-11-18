namespace AttendanceTracker.Features.Work.Interfaces
{
    public interface ITimeOutCommandHandler
    {
        Task Handle(int employeeId);
    }
}
