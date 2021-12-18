using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using mesi.Models;

namespace mesi.DataAccess
{
    public class UsersRepository
    {
        readonly string _connectionString;

        public UsersRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<Users> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var users = db.Query<Users>(@"SELECT * FROM Users");
            return users;
        }

        internal void AddNewUser(Users newUser)
        {
            using var db = new SqlConnection(_connectionString);
            Guid id = new Guid();
            var sql = @"INSERT INTO [dbo].[Users]
                        ([FirstName],
                         [LastName],
                         [ProfilePicture],
                         [Email])
                            OUTPUT inserted.Id
                            VALUES
                         (@FirstName,
                          @LastName,
                          @ProfilePicture,
                          @Email)";


            id = db.ExecuteScalar<Guid>(sql, newUser);
            newUser.Id = id;
        }

        internal Users GetUserById(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Users WHERE Id = @Id";
            var user = db.QuerySingleOrDefault<Users>(sql, new { id = userId });
            if (user == null) return null;
            return user;
        }

        internal Users GetByEmail(string email)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"Select * From Users where Email = @Email";
            var parameter = new
            {
                Email = email
            };
            var user = db.QuerySingleOrDefault<Users>(sql, parameter);
            return user;
        }
    }
}
