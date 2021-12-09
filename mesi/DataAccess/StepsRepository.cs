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
    public class StepsRepository
    {
        readonly string _connectionString;

        public StepsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<Steps> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var steps = db.Query<Steps>(@"SELECT * FROM Steps");
            return steps;
        }

        internal Steps GetStepById(Guid stepId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Steps WHERE Id = @Id";
            var step = db.QuerySingleOrDefault<Steps>(sql, new { id = stepId });
            if (step == null) return null;
            return step;
        }

    }
}
