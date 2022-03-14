using Microsoft.Data.SqlClient;
using Module13.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module13.Repositories
{
    public class ProductRepository : Repository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task InsertProductAsync(Product product)
        {
            string query = $"INSERT INTO Product VALUES ('{product.Name}', '{product.Description}', {product.Weight}, {product.Height}, {product.Width}, {product.Length})";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task UpdateProductByNameAsync(string name, Product product)
        {
            string query = $"UPDATE Product SET Name = '{product.Name}', Description = '{product.Description}', Weight = {product.Weight}, Height = {product.Height}, Width = {product.Width}, Length = {product.Length} WHERE Name = '{name}'";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task DeleteProductByNameAsync(string name)
        {
            string query = $"DELETE Product Where Name = '{name}'";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task DeleteAllProductsAsync()
        {
            string query = $"DELETE Product";

            await this.ExecuteNonQueryQueryAsync(query);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            string query = "SELECT * FROM Product";

            using SqlConnection connection = new SqlConnection(this.connectionString);
            using SqlDataReader reader = await this.ExecuteDataReaderQueryAsync(query, connection);

            List<Product> result = new List<Product>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Product product = new Product()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Weight = (int)reader["Weight"],
                        Height = (int)reader["Height"],
                        Width = (int)reader["Width"],
                        Length = (int)reader["Length"]
                    };

                    result.Add(product);
                }
            }

            return result;
        }
    }
}
