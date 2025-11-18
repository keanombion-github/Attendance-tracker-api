namespace AttendanceTracker.Features.Auth.Interfaces
{
    public interface IRegisterCommandHandler
    {
        Task<(bool Success, string Message, int EmployeeId)> Handle(RegisterRequest request);
    }
}
