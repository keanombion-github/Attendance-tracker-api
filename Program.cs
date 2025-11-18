using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AttendanceTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Database(connectionString!));
builder.Services.AddScoped<AttendanceTracker.Features.Auth.Interfaces.IRegisterCommandHandler, AttendanceTracker.Features.Auth.Handlers.RegisterCommandHandler>();
builder.Services.AddScoped<AttendanceTracker.Features.Auth.Interfaces.ILoginCommandHandler, AttendanceTracker.Features.Auth.Handlers.LoginCommandHandler>();
builder.Services.AddScoped<AttendanceTracker.Features.Work.Interfaces.ITimeInCommandHandler, AttendanceTracker.Features.Work.Handlers.TimeInCommandHandler>();
builder.Services.AddScoped<AttendanceTracker.Features.Work.Interfaces.ITimeOutCommandHandler, AttendanceTracker.Features.Work.Handlers.TimeOutCommandHandler>();
builder.Services.AddScoped<AttendanceTracker.Features.Admin.Interfaces.IGetReportQueryHandler, AttendanceTracker.Features.Admin.Handlers.GetReportQueryHandler>();
builder.Services.AddScoped<AttendanceTracker.Features.Admin.Interfaces.IGetAllUsersQueryHandler, AttendanceTracker.Features.Admin.Handlers.GetAllUsersQueryHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? builder.Configuration["Jwt:Secret"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Disabled for Render deployment

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Apply database schema on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var database = services.GetRequiredService<Database>();
    var setup = new Setup(database);
    try
    {
        setup.ApplySchema();
        Console.WriteLine("Database schema applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Warning: Could not connect to database: {ex.Message}");
        Console.WriteLine("Application will start but database operations will fail.");
    }
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"Application starting on port {port}...");
app.Run($"http://0.0.0.0:{port}");