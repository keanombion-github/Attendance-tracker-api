namespace AttendanceTracker.Features.Admin.Interfaces
{
    public interface IGetAllUsersQueryHandler
    {
        Task<Users> Handle();
    }
}