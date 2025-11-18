using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Admin.Interfaces;

namespace AttendanceTracker.Features.Admin.Handlers
{
    public class GetAllUsersQueryHandler : IGetAllUsersQueryHandler
    {
        private readonly Database _database;

        public GetAllUsersQueryHandler(Database database)
        {
            _database = database;
        }

        public async Task<Users> Handle()
        {
            using var connection = _database.CreateConnection();
            var users = await connection.QueryAsync<User>(
                @"SELECT Id, firstName, lastName, email, isAdmin
                  FROM employees
                  ORDER BY Id");
            return new Users(users);
        }
    }
}