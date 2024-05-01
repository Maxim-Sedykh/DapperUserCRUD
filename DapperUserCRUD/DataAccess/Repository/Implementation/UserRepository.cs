using Dapper;
using DapperUserCRUD.DataAccess.Repository.Interface;
using DapperUserCRUD.Objects.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Data;

namespace DapperUserCRUD.DataAccess.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task CreateAsync(User user)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            await db.ExecuteAsync(@"INSERT INTO Users (Id, Login, Password, Email, Age, CreatedAt)
                VALUES(@Id, @Login, @Password, @Email, @Age, @CreatedAt)", user);
        }

        public async Task DeleteAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            await db.ExecuteAsync("DELETE FROM Users WHERE Id = @id", new { id });
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            return (await db.QueryAsync<User>("SELECT * FROM Users")).ToList();
        }

        public async Task UpdateAsync(User user)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            await db.ExecuteAsync("UPDATE Users SET Login = @Login, Age = @Age, Email = @Email WHERE Id = @Id", user);
        }
    }
}
