using Baseball.Data.Sql;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Basebll.Data.Sql.IntegrationTests
{
    class SqlSecurityTests
    {

        private const string connectionString = "Server=.Database=Baseball;Integrated_Security=true;";

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
