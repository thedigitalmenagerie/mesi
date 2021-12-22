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

        internal Household GetHousehold(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Households
                            WHERE Id = @Id";
            var parameter = new
            {
                Id = id,
            };
            var result = db.QuerySingleOrDefault<Household>(sql, parameter);
            return result;
        }

        internal void AddHousehold(Household household)
        {
            using var db = new SqlConnection(_connectionString);
            Guid id = new Guid();

            var sql = @"INSERT INTO [dbo].[Households]
                        ([HouseholdName], 
                         [HasPets],
                         [HasKids],
                         [HasRomance],
                         [StepId])
                        OUTPUT inserted.Id
                        VALUES
                       (@HouseholdName,
                        @HasPets,
                        @HasKids,
                        @HasRomance,
                        @StepId)";


            id = db.ExecuteScalar<Guid>(sql, household);
            household.Id = id;
        }

        internal Household EditHousehold(Guid id, Household household)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Households
                        SET HouseholdName = @HouseholdName,
                            HasPets = @HasPets,
                            HasKids = @HasKids,
                            HasRomance = @HasRomance,
                            StepId = @StepId
                        WHERE Id = @Id";

            household.Id = id;
            var updatedCard = db.QuerySingleOrDefault<Household>(sql, household);

            return household;
        }

        internal bool DeleteHousehold(Guid id)
        {
            bool returnVal = false;
            using var db = new SqlConnection(_connectionString);
            var sql = @"DELETE FROM Households
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
