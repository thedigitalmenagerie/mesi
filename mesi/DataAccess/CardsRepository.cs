using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using mesi.Models;
using Microsoft.Extensions.Configuration;

namespace mesi.DataAccess
{
    public class CardsRepository
    {
        readonly string _connectionString;

        public CardsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<CardsWithDetail> GetCardsWithDetails(Guid householdId, Guid userId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT C.Id AS HHCardId, C.HouseholdId, C.NeedTypeId, C.CategoryTypeId, C.CardName,
				                C.CardImage, C.CardDefinition, C.Conception, C.Planning, C. Execution, C.MSOC, C.DailyGrind,
				                UV.Id as UVId, UV.UserValues, UV.UserDeletes,  U.Id as UserId, U.FirstName, U.ProfilePicture, N.NeedTypeName, CT.CategoryTypeName
	                        FROM Cards C
		                        JOIN UserValues UV ON UV.CardId = C.Id
		                        JOIN Users U ON U.Id = UV.UserId
		                        JOIN NeedTypes N ON N.Id = C.NeedTypeId
		                        JOIN CategoryTypes CT ON CT.Id = C.CategoryTypeId
                            WHERE HouseholdId = @HouseholdId AND UserId = @UserId";
            var parameter = new
            {
                HouseholdId = householdId,
                UserId = userId
            };
            var result = db.Query<CardsWithDetail>(sql, parameter);
            return result;
        }
    }
}
