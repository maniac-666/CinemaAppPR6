using CinemaApp.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void RegisterTestSuccess()
        {
            var page = new RegisterPage();

            string unique = DateTime.Now.Ticks.ToString();

            Assert.IsTrue(page.Register(
                "Тестов Тест Тестович",
                "testuser_" + unique,
                "testpass123"
            ));

            Assert.IsTrue(page.Register(
                "Иванов Иван Иванович",
                "ivanov_" + unique,
                "ivanpass456"
            ));

            Assert.IsTrue(page.Register(
                "Петрова Мария Сергеевна",
                "petrova_" + unique,
                "mariapass789"
            ));
        }

        [TestMethod]
        public void RegisterTestFail()
        {
            var page = new RegisterPage();

            Assert.IsFalse(page.Register("", "", ""));
            Assert.IsFalse(page.Register("", "newlogin", "pass123"));
            Assert.IsFalse(page.Register("Иванов Иван", "", "pass123"));
            Assert.IsFalse(page.Register("Иванов Иван", "newlogin", ""));

            Assert.IsFalse(page.Register(" ", " ", " "));
            Assert.IsFalse(page.Register("Иванов Иван", "  ", "pass123"));
            Assert.IsFalse(page.Register("Иванов Иван", "login1", "   "));

            Assert.IsFalse(page.Register("Новый Пользователь", "admin", "newpass123"));
        }
    }
}
