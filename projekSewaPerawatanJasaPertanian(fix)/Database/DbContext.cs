using DotNetEnv;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekSewaPerawatanJasaPertanian_fix_.Database
{
    public class DbContext
    {
        public string connStr;

        public DbContext()
        {
            Env.Load(); // pastikan .env ada di root project
            connStr = Environment.GetEnvironmentVariable("CONN_STR");
        }

        // Instance method: panggil dengan `new DbContext().GetConnection()`
        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connStr);
            return conn;
        }
    }
}
