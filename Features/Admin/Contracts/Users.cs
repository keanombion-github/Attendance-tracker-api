namespace AttendanceTracker.Features.Admin
{
    public class User(int Id, string FirstName, string LastName, string Role);
    public record Users(IEnumerable<User> Items);
}