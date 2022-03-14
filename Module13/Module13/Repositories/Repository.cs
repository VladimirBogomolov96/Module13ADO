using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module13.Repositories
{
    public abstract class Repository
    {
        protected readonly string connectionString;

        public Repository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected async Task ExecuteNonQueryQueryAsync(string query)
        {
            using SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            await sqlConnection.OpenAsync();
            await sqlCommand.ExecuteNonQueryAsync();
        }

        protected async Task<SqlDataReader> ExecuteDataReaderQueryAsync(string query, SqlConnection connection)
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(query, connection);

            return await command.ExecuteReaderAsync();
        }
    }
}
