
using System;
using Oracle.ManagedDataAccess.Client;

namespace FutbolSolution.Repository
{
    public class AppDbContext : IDisposable
    {
        private readonly string _connectionString;
        private OracleConnection _connection;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new OracleConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if(_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public OracleCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void CloseConnection()
        {
            if(_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            _connection.Dispose();
        }
    }
}
