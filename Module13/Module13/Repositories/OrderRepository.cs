using Microsoft.Data.SqlClient;
using Module13.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module13.Repositories
{
    public class OrderRepository : Repository 
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task InsertOrderAsync(Order order)
        {
            string format = "yyyy-MM-dd";

            string query = $"INSERT INTO [Order] VALUES ({(int)order.Status}, '{order.CreatedDate.ToString(format)}', '{order.UpdatedDate.ToString(format)}', {order.ProductId})";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task UpdateOrderByIdAsync(int id, Order order)
        {
            string format = "yyyy-MM-dd";

            string query = $"UPDATE [Order] SET Status = {(int)order.Status}, CreatedDate = '{order.CreatedDate.ToString(format)}', UpdatedDate = '{order.UpdatedDate.ToString(format)}', ProductId = {order.ProductId} WHERE Id = '{id}'";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task DeleteAllOrdersAsync()
        {
            string query = $"DELETE [Order]";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            string query = "SELECT * FROM [Order]";

            using SqlConnection connection = new SqlConnection(this.connectionString);
            using SqlDataReader reader = await this.ExecuteDataReaderQueryAsync(query, connection);

            return await this.GetOrdersFromReaderAsync(reader);
        }

        public async Task<List<Order>> GetOrdersByMonthAsync(int monthNumber)
        {
            string procedureName = "GetOrdersByMonth";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure };

            SqlParameter monthNumberParam = new SqlParameter
            {
                ParameterName = "@MonthNumber",
                Value = monthNumber
            };

            command.Parameters.Add(monthNumberParam);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            return await this.GetOrdersFromReaderAsync(reader);
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatus orderStatus)
        {
            string procedureName = "GetOrdersByStatus";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure };

            SqlParameter orderStatusParam = new SqlParameter
            {
                ParameterName = "@Status",
                Value = orderStatus
            };

            command.Parameters.Add(orderStatusParam);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            return await this.GetOrdersFromReaderAsync(reader);
        }

        public async Task<List<Order>> GetOrdersByYearAsync(int year)
        {
            string procedureName = "GetOrdersByYear";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure };

            SqlParameter yearParam = new SqlParameter
            {
                ParameterName = "@Year",
                Value = year
            };

            command.Parameters.Add(yearParam);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            return await this.GetOrdersFromReaderAsync(reader);
        }

        public async Task<List<Order>> GetOrdersByProductIdAsync(int productId)
        {
            string procedureName = "GetOrdersByProductId";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure };

            SqlParameter productIdParam = new SqlParameter
            {
                ParameterName = "@ProductId",
                Value = productId
            };

            command.Parameters.Add(productIdParam);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            return await this.GetOrdersFromReaderAsync(reader);
        }

        public async Task DeleteOrdersByMonthAsync(int monthNumber)
        {
            string procedureName = "DeleteOrdersByMonth";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = transaction };

            SqlParameter monthParam = new SqlParameter
            {
                ParameterName = "@MonthNumber",
                Value = monthNumber
            };

            command.Parameters.Add(monthParam);

            try
            {
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteOrdersByStatusAsync(OrderStatus orderStatus)
        {
            string procedureName = "DeleteOrdersByStatus";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = transaction };

            SqlParameter orderStatusParam = new SqlParameter
            {
                ParameterName = "@Status",
                Value = orderStatus
            };

            command.Parameters.Add(orderStatusParam);

            try
            {
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteOrdersByYearAsync(int year)
        {
            string procedureName = "DeleteOrdersByYear";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = transaction };

            SqlParameter orderStatusParam = new SqlParameter
            {
                ParameterName = "@Year",
                Value = year
            };

            command.Parameters.Add(orderStatusParam);

            try
            {
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteOrdersByProductIdAsync(int productId)
        {
            string procedureName = "DeleteOrdersByProductId";

            using SqlConnection connection = new SqlConnection(this.connectionString);

            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = System.Data.CommandType.StoredProcedure, Transaction = transaction };

            SqlParameter orderStatusParam = new SqlParameter
            {
                ParameterName = "@ProductId",
                Value = productId
            };

            command.Parameters.Add(orderStatusParam);

            try
            {
                await command.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<List<Order>> GetOrdersFromReaderAsync(SqlDataReader reader)
        {
            List<Order> result = new List<Order>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Order order = new Order()
                    {
                        Id = (int)reader["Id"],
                        Status = (OrderStatus)reader["Status"],
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        UpdatedDate = (DateTime)reader["UpdatedDate"],
                        ProductId = (int)reader["ProductId"]
                    };

                    result.Add(order);
                }
            }

            return result;
        }
    }
}
