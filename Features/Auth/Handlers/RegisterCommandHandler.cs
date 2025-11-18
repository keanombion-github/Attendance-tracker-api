using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Auth.Interfaces;

namespace AttendanceTracker.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRegisterCommandHandler
    {
        private readonly Database _database;

        public RegisterCommandHandler(Database database)
        {
            _database = database;
        }

        public async Task<(bool Success, string Message, int EmployeeId)> Handle(RegisterRequest request)
        {
            using var connection = _database.CreateConnection();
            
            var exists = await connection.ExecuteScalarAsync<bool>(
                "SELECT EXISTS(SELECT 1 FROM Employees WHERE Email = @Email)",
                new { request.Email });
            
            if (exists)
                return (false, "Email already registered", 0);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                IsAdmin = request.IsAdmin
            };

            var id = await connection.ExecuteScalarAsync<int>(
                "INSERT INTO Employees (FirstName, LastName, Email, PasswordHash, IsAdmin) VALUES (@FirstName, @LastName, @Email, @PasswordHash, @IsAdmin) RETURNING Id",
                employee);
            return (true, "User registered successfully", id);
        }
    }
}
