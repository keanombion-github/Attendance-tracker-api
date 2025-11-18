namespace AttendanceTracker.Data
{
    public static class Schema
    {
        public const string CreateTables = @"
            CREATE TABLE IF NOT EXISTS Employees (
                Id SERIAL PRIMARY KEY,
                FirstName VARCHAR(50) NOT NULL,
                LastName VARCHAR(50) NOT NULL,
                Email VARCHAR(100) NOT NULL UNIQUE,
                PasswordHash VARCHAR(255) NOT NULL,
                IsAdmin BOOLEAN NOT NULL DEFAULT FALSE
            );

            CREATE TABLE IF NOT EXISTS TimeRecords (
                Id SERIAL PRIMARY KEY,
                EmployeeId INT NOT NULL REFERENCES Employees(Id),
                TimeIn TIMESTAMPTZ NOT NULL,
                TimeOut TIMESTAMPTZ
            );
        ";
    }
}
