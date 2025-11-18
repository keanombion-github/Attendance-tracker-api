using Dapper;
using Npgsql;

namespace AttendanceTracker.Data
{
    public class Setup
    {
        private readonly Database _database;

        public Setup(Database database)
        {
            _database = database;
        }

        public void ApplySchema()
        {
            using var connection = _database.CreateConnection();
            connection.Execute(Schema.CreateTables);
        }
    }
}
