using CinemaApp.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void AuthTest()
        {
            var page = new LoginPage();

            Assert.IsTrue(page.Auth("admin", "123"));
            Assert.IsFalse(page.Auth("user1", "12345"));
            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
        }

        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new LoginPage();

            Assert.IsTrue(page.Auth("admin", "123"));
            Assert.IsTrue(page.Auth("user1", "123"));
            Assert.IsTrue(page.Auth("666", "555"));
            Assert.IsTrue(page.Auth("123", "123"));
        }

        [TestMethod]
        public void AuthTestFail()
        {
            var page = new LoginPage();

            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth("admin", ""));
            Assert.IsFalse(page.Auth("", "123"));

            Assert.IsFalse(page.Auth(" ", " "));
            Assert.IsFalse(page.Auth("   ", "123"));
            Assert.IsFalse(page.Auth("admin", "   "));

            Assert.IsFalse(page.Auth("hacker", "hack123"));
            Assert.IsFalse(page.Auth("nouser", "nopass"));

            Assert.IsFalse(page.Auth("admin", "wrongpass"));
            Assert.IsFalse(page.Auth("user1", "456"));
            Assert.IsFalse(page.Auth("666", "777"));

            Assert.IsFalse(page.Auth("admin999", "123"));
            Assert.IsFalse(page.Auth("user999", "555"));

            Assert.IsFalse(page.Auth("!@#$%", "test"));
            Assert.IsFalse(page.Auth("' OR 1=1 --", "test"));
            Assert.IsFalse(page.Auth(new string('x', 500), new string('y', 500)));
        }
    }
}