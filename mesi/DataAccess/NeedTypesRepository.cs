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
    public class NeedTypesRepository
    {
        readonly string _connectionString;

        public NeedTypesRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<NeedTypes> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var needTypes = db.Query<NeedTypes>(@"SELECT * FROM NeedTypes");
            return needTypes;
        }

        internal NeedTypes GetNeedTypeById(Guid needTypeId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM NeedTypes WHERE Id = @Id";
            var needType = db.QuerySingleOrDefault<NeedTypes>(sql, new { id = needTypeId });
            if (needType == null) return null;
            return needType;
        }

    }
}