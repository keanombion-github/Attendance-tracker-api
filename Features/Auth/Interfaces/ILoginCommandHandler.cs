namespace AttendanceTracker.Features.Auth.Interfaces
{
    public interface ILoginCommandHandler
    {
        Task<string?> Handle(LoginRequest request);
    }
}
