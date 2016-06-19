using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentalSite;
using RentalSite.Controllers;
using System.Data.SqlClient;

namespace RentalSite.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void makeAccountDB()
        {
            SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;Integrated Security=True;");
            using (connection)
            {
                connection.Open();

                string sql = string.Format(@"
        CREATE DATABASE
            [Accounts]
        ON PRIMARY (
           NAME=Accounts_data ,
           FILENAME = '{0}\Accounts.mdf'
        )",
                  @"D:\SkyDrive\GAME SAVES\SourceCode\RentalSite\RentalSite\RentalSite\TestDb"
                );

                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void makeHHLSQLDB()
        {
            SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;Integrated Security=True;");
            using (connection)
            {
                connection.Open();

                string sql = string.Format(@"
        CREATE DATABASE
            [HHLSQL]
        ON PRIMARY (
           NAME=HHLSQL_data ,
           FILENAME = '{0}\HHLSQL.mdf'
        )",
                  @"D:\SkyDrive\GAME SAVES\SourceCode\RentalSite\RentalSite\RentalSite\TestDb"
                );

                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
