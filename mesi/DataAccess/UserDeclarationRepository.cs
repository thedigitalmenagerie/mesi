using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using mesi.Models;
using System.Linq;

namespace mesi.DataAccess
{
    public class UserDeclarationRepository
    {
        readonly string _connectionString;

        public UserDeclarationRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal UserDeclaration GetById(Guid Id)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM UserDeclaration
                                WHERE Id = @Id";

            var parameter = new
            {
                Id = Id,
            };

            var result = db.QueryFirstOrDefault<UserDeclaration>(sql, parameter);
            return result;
        }

        internal UserDeclaration GetByUserandCard(Guid userId, Guid cardId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM UserDeclaration
                                WHERE UserId = @UserId AND CardId = @CardId";

            var parameter = new
            {
                UserId = userId,
                CardId = cardId
            };

            var result = db.QueryFirstOrDefault<UserDeclaration>(sql, parameter);
            return result;
        }

        internal IEnumerable<UserDeclaration> GetByCard(Guid cardId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM UserDeclaration
                                WHERE CardId = @CardId";

            var parameter = new
            {
                CardId = cardId
            };

            var result = db.Query<UserDeclaration>(sql, parameter);
            return result;
        }

        internal IEnumerable<UserDeclarationWithPicture> GetByCardUV(Guid cardId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT D.Id, D.UserId, D.UserValues, D.UserDeletes, U.ProfilePicture FROM UserDeclaration D
	                        JOIN Users U on U.Id = D.UserId
	                    WHERE D.UserValues = 1
                        AND D.CardId = @CardId";

            var parameter = new
            {
                CardId = cardId
            };

            var result = db.Query<UserDeclarationWithPicture>(sql, parameter);
            return result;
        }

        internal IEnumerable<UserDeclaration> GetByUser(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM UserDeclaration
                                WHERE UserId = @UserId";

            var parameter = new
            {
                UserId = userId
            };

            var result = db.Query<UserDeclaration>(sql, parameter);
            return result;
        }

        internal void AddUserDeclaration(UserDeclaration userDeclaration)
        {
            using var db = new SqlConnection(_connectionString);
            Guid id = new Guid();

            var sql = @"INSERT INTO [dbo].[UserDeclaration]
                        ([UserId],
                         [CardId],
                         [UserValues],
                         [UserDeletes])
                        OUTPUT inserted.Id
                        VALUES
                       (@UserId,
                        @CardId,
                        @UserValues,
                        @UserDeletes)";

            id = db.ExecuteScalar<Guid>(sql, userDeclaration);
            userDeclaration.Id = id;
        }

        internal UserDeclaration EditUserDeclaration(Guid id, UserDeclaration userDeclaration)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE UserDeclaration
                        SET UserId = @UserId, 
                            CardId = @CardId,
                            UserValues = @UserValues,
                            UserDeletes = @UserDeletes
                        WHERE Id = @Id";

            userDeclaration.Id = id;
            var updatedUserDeclaration = db.QuerySingleOrDefault<UserDeclaration>(sql, userDeclaration);

            return updatedUserDeclaration;
        }

        internal bool DeleteUserDeclaration(Guid id)
        {
            bool returnVal = false;
            using var db = new SqlConnection(_connectionString);
            var sql = @"DELETE FROM UserDeclaration
                        OUTPUT Deleted.Id
                        WHERE Id = @Id";
            var parameters = new
            {
                Id = id
            };

            var result = db.Query(sql, parameters);
            if (result.Count() > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }
    }
}
