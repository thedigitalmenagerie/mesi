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
    public class HouseholdMembersRepository
    {
        readonly string _connectionString;

        public HouseholdMembersRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<HouseholdMembers> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var householdMembers = db.Query<HouseholdMembers>(@"SELECT * FROM HouseholdMembers");
            return householdMembers;
        }

        internal IEnumerable<HouseholdMembers> GetById(Guid id)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM HouseholdMembers
                            WHERE Id = @Id";
            var parameter = new
            {
                Id = id,
            };
            var result = db.Query<HouseholdMembers>(sql, parameter);
            return result;
        }

        internal IEnumerable<HouseholdMembers> GetByHouseholdId(Guid householdId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM HouseholdMembers
                            WHERE HouseholdId = @HouseholdId";
            var parameter = new
            {
                HouseholdId = householdId,
            };
            var result = db.Query<HouseholdMembers>(sql, parameter);
            return result;
        }

        internal IEnumerable<HouseholdMembers> GetByUserId(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"SELECT * FROM HouseholdMembers
                            WHERE UserId = @UserId";
            var parameter = new
            {
                UserId = userId,
            };
            var result = db.Query<HouseholdMembers>(sql, parameter);
            return result;
        }

        internal void AddNewHouseholdMembers(HouseholdMembers householdMembers)
        {
            using var db = new SqlConnection(_connectionString);
            Guid id = new Guid();
            var sql = @"INSERT INTO [dbo].[HouseholdMembers]
                        ([UserId],
                         [HouseholdId],
                         [CommunityAgreement],
                         [NewVow],
                         [Redeal])
                            OUTPUT inserted.Id
                            VALUES
                         (@UserId,
                          @HouseholdId,
                          @CommunityAgreement,
                          @NewVow,
                          @Redeal)";


            id = db.ExecuteScalar<Guid>(sql, householdMembers);
            householdMembers.Id = id;
        }

        internal HouseholdMembers EditHouseholdMember(Guid id, HouseholdMembers householdMembers)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE HouseholdMembers
                        SET UserId = @UserId, 
                            HouseholdId = @HouseholdId,
                            CommunityAgreement = @CommunityAgreement,
                            NewVow = @NewVow,
                            Redeal = @Redeal
                        WHERE Id = @Id";

            householdMembers.Id = id;
            var updatedHouseholdMember = db.QuerySingleOrDefault<HouseholdMembers>(sql, householdMembers);

            return updatedHouseholdMember;
        }
    }
}
