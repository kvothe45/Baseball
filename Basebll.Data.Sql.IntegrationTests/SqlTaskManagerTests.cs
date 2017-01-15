using Baseball.Data.Sql;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Linq;

namespace Basebll.Data.Sql.IntegrationTests
{
    class SqlTaskManagerTests
    {
        private const string connectionString = "Server=.;Database=Baseball;Integrated Security=true;";

        [Test]
        public void FullCycleTest()
        {
            ClearAllTasks();
            var manager = new SqlTaskManager(connectionString);

            Assert.AreEqual(0, manager.GetAll().Count());
            manager.Add("Task1");
            manager.Add("Task2");
            manager.Add("Task3");
            Assert.AreEqual(3, manager.GetAll().Count());
            manager.Add("Task4");
            var all = manager.GetAll().ToList();
            Assert.AreEqual(4, all.Count);
            var fourth = all[3];
            Assert.AreEqual("Task4", fourth.Name);
            Assert.AreEqual(false, fourth.IsComplete);
            manager.Remove(fourth.TaskItemId);
            all = manager.GetAll().ToList();
            Assert.AreEqual(3, all.Count);
            var second = all[1];
            manager.Complete(second.TaskItemId);
            all = manager.GetAll().ToList();
            second = all.Where(i => i.TaskItemId == second.TaskItemId).Single();
            Assert.True(second.IsComplete);

        }

        private void ClearAllTasks()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var com = new SqlCommand("DELETE TaskItem", con))
                {
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}
