using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FutbolSolution.Repository.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _appDbContext;

        protected BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected async Task<List<T>> ExecuteReaderAsync<T>(
            string commandText,
            OracleParameter[] parameters,
            Func<IDataReader, T> mapFunction)
        {
            var results = new List<T>();

            using (var command = _appDbContext.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                _appDbContext.OpenConnection();
                using (var reader = (OracleDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(mapFunction(reader));
                    }
                }
            }

            return results;
        }

        protected async Task<T> ExecuteReaderSingleAsync<T>(
            string commandText,
            OracleParameter[] parameters,
            Func<IDataReader, T> mapFunction)
        {
            T result = default;

            using (var command = _appDbContext.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                _appDbContext.OpenConnection();
                using (var reader = (OracleDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        result = mapFunction(reader);
                    }
                }
            }

            return result;
        }

        protected async Task ExecuteNonQueryAsync(string commandText, OracleParameter[] parameters)
        {
            using (var command = _appDbContext.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                _appDbContext.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
        }

        // Helper method to get a value from the reader
        protected T GetValue<T>(IDataReader reader, string columnName)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentException("Column name cannot be null or empty.", nameof(columnName));

            int ordinal = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(ordinal))
            {
                return default(T);
            }

            object value = reader.GetValue(ordinal);

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
