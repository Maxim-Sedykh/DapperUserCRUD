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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"INSERT INTO [Users]
                                 ([Id]
                                 ,[Login]
                                 ,[Password]
                                 ,[Email]
                                 ,[Age]
                                 ,[CreatedAt])
                            VALUES
                                (@Id
                                 ,@Login
                                 ,@Password
                                 ,@Email
                                 ,@Age
                                 ,@CreatedAt)";

                await db.ExecuteAsync(sql, user);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"DELETE 
                            FROM 
                                [Users]
                            WHERE 
                                [Id] = @id";

                await db.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT 
                                [Id]
                               ,[Login]
                               ,[Password]
                               ,[Email]
                               ,[Age]
                               ,[CreatedAt]
                            FROM
                                [Users]
                            WHERE
                                [Id]=@Id";

                return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT 
                                [Id]
                               ,[Login]
                               ,[Password]
                               ,[Email]
                               ,[Age]
                               ,[CreatedAt]
                            FROM
                                [Users]";

                return await db.QueryAsync<User>(sql);
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"UPDATE [Users]
                            SET
                                 [Login] = @Login
                                ,[Age] = @Age
                                ,[Email] = @Email
                            WHERE
                                [Id]=@Id";

                await db.ExecuteAsync(sql, user);
            }
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT 
                                [Id]
                               ,[Login]
                               ,[Password]
                               ,[Email]
                               ,[Age]
                               ,[CreatedAt]
                            FROM
                                [Users]
                            WHERE
                                [Login]=@Login";

                return await db.QueryFirstOrDefaultAsync<User>(sql, new { Login = login });
            }
        }
    }
}
