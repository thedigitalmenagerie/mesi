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
    public class CategoryTypesRepository
    {
        readonly string _connectionString;

        public CategoryTypesRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<CategoryTypes> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var categoryTypes = db.Query<CategoryTypes>(@"SELECT * FROM CategoryTypes");
            return categoryTypes;
        }

        internal CategoryTypes GetCategoryTypeById(Guid categoryTypeId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM CategoryTypes WHERE Id = @Id";
            var categoryType = db.QuerySingleOrDefault<CategoryTypes>(sql, new { id = categoryTypeId });
            if (categoryType == null) return null;
            return categoryType;
        }

    }
}
