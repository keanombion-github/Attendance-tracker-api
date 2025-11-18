using Dapper;
using AttendanceTracker.Data;
using AttendanceTracker.Features.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AttendanceTracker.Features.Auth.Handlers
{
    public class LoginCommandHandler : ILoginCommandHandler
    {
        private readonly Database _database;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(Database database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        public async Task<string?> Handle(LoginRequest request)
        {
            using var connection = _database.CreateConnection();
            var employee = await connection.QuerySingleOrDefaultAsync<Employee>("SELECT * FROM Employees WHERE Email = @Email", new { request.Email });

            if (employee == null || !BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, employee.IsAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
