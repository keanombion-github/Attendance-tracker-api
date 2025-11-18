namespace AttendanceTracker.Features.Auth
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, bool IsAdmin);
}
