using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Baseball.Data.Sql
{
    public class SqlTaskManager : ITaskManager
    {
        private readonly string connectionString;

        public SqlTaskManager(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        public void Add(string name)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "INSERT INTO TaskItem (Name, IsComplete) VALUES (@Name, 0)";
                    com.Parameters.AddWithValue("@Name", name);
                    com.ExecuteNonQuery();
                }
            }

        }

        public void Remove(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "DELETE TaskItem WHERE TaskItemId = @Id";
                    com.Parameters.AddWithValue("@Id", id);
                    com.ExecuteNonQuery();
                }
            }
        }

        public void Complete(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "UPDATE TaskItem SET IsComplte = 1 WHERE TaskItemId = @Id";
                    com.Parameters.AddWithValue("@Id", id);
                    com.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<TaskItem> GetAll()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "SELECT * FROM TaskItem";
                    using (var rdr = com.ExecuteReader())
                    {
                        var list = new List<TaskItem>();
                        while (rdr.Read())
                        {
                            var item = new TaskItem();
                            item.TaskItemId = rdr.GetInt32(0);
                            item.Name = rdr.GetString(1);
                            item.IsComplete = rdr.GetBoolean(2);
                            list.Add(item);
                        }
                        return list;
                    }
                }
            }
        }
    }
}
