using Baseball.Data.Sql;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Basebll.Data.Sql.IntegrationTests
{
    class SqlSecurityTests
    {
        //This is the generic line for the current active server when you only have one instance running
        //private const string connectionString = "Server=.;Database=Baseball;Integrated Security=true;";
        private const string connectionString = "Server=DESKTOP-DGL4MS0;Database=Baseball;Integrated Security=true;";

        [Test]
        public void WhenUserDoesNotExist_ThrowsException()
        {
            var security = new SqlSecurity(connectionString);
            Assert.Throws<InvalidOperationException>(() =>
            {
                security.Authenticate("FAKEUSERNAME", "FAKEPASSWORD");
            });
        }

        [Test]
        public void WhenPasswordIsWrong_ThrowsException()
        {
            var security = new SqlSecurity(connectionString);
            Assert.Throws<InvalidOperationException>(() =>
            {
                security.Authenticate("Bravo", "FAKEPASSWORD");
            });
        }

        [Test]
        public void WhenUsernameAndPasswordAreCorrect_ReturnsCorrectPerson()
        {
            var security = new SqlSecurity(connectionString);
            var person = security.Authenticate("Charlie", "pass3");
            Assert.AreEqual(3, person.PersonId);
            Assert.AreEqual(true, person.IsPlayer);
            Assert.AreEqual(false, person.IsCaptain);
            Assert.AreEqual("is so so awesome", person.DisplayName);
        }

    }
}
