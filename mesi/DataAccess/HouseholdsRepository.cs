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
    public class HouseholdsRepository
    {
        readonly string _connectionString;

        public HouseholdsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<HouseholdWithDetail> GetHouseholdWithDetails(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT U.Id as UserId, U.ProfilePicture, U.FirstName,
		                        H.Id as HouseholdId, H.HouseholdName, H.HasPets, H.HasKids, H.HasRomance, S.StepName
	                                FROM Users U
	                            JOIN HouseholdMembers HHM ON HHM.UserId = U.Id
	                            JOIN Households H ON H.Id = HHM.HouseholdId
	                            JOIN Steps S on S.Id = H.StepId
                                    WHERE UserId = @UserId";
            var parameter = new
            {
                UserId = userId
            };
            var result = db.Query<HouseholdWithDetail>(sql, parameter);
            return result;
        }
    }
}
