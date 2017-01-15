using System;
using System.Data.SqlClient;

namespace Baseball.Data.Sql
{
    public class SqlSecurity : ISecurity
    {
        private readonly string connectionString;

        public SqlSecurity(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        public Person Authenticate(string username, string password)
        {

            using (var con = new SqlConnection(connectionString))
            {

            }

            throw new NotImplementedException();
        }
    }
}
