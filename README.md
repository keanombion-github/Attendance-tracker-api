# Attendance Tracker API

A .NET 9.0 Web API for tracking employee attendance with JWT authentication.

## Features

- Employee registration and authentication
- Time in/out tracking
- Admin reports and user management
- PostgreSQL database integration
- JWT token-based security

## Local Development

1. Install .NET 9.0 SDK
2. Set up PostgreSQL database
3. Update `appsettings.json` with your database connection
4. Run the application:
   ```bash
   dotnet run
   ```

## Environment Variables (Production)

- `DATABASE_URL`: PostgreSQL connection string
- `JWT_SECRET`: Secret key for JWT token signing
- `PORT`: Port number (default: 8080)

## API Endpoints

- `POST /api/auth/register` - Register new employee
- `POST /api/auth/login` - Employee login
- `POST /api/work/timein` - Clock in
- `POST /api/work/timeout` - Clock out
- `GET /api/admin/users` - Get all users (admin only)
- `GET /api/admin/report` - Get attendance report (admin only)

## Deployment

This application is configured for deployment on Render.com using Docker.
