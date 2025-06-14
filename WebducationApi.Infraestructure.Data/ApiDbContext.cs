using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Infraestructure.Data
{
    public class ApiDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _sqlConnection;

        public ApiDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = configuration.GetConnectionString("MyLocalConnection");
        }

        public IDbConnection CreateSqlConnection()
        {
            return new SqlConnection(_sqlConnection);
        }
    }
}
