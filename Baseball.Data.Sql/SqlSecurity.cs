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
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "SELECT * FROM Person WHERE Username = @Username AND Password = @Password";
                    com.Parameters.AddWithValue("@Username", username);
                    com.Parameters.AddWithValue("@Password", password);
                    using (var rdr = com.ExecuteReader())
                    {
                        if (!rdr.HasRows)
                        {
                            throw new InvalidOperationException();
                        }
                        rdr.Read();
                        var person = new Person();
                        person.PersonId = rdr.GetInt32(0);
                        person.IsPlayer = rdr.GetBoolean(3);
                        person.IsCaptain = rdr.GetBoolean(4);
                        person.DisplayName = rdr.GetString(5);
                        return person;

                    }

                }
            }

            //throw new NotImplementedException();
        }
    }
}
