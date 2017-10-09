using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Capstone.CLI;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Capstone.Tests
{
    [TestClass]
    public class CampsiteDALTests
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";
        TransactionScope t;

        [TestInitialize]
        public void Initialize()
        {
            //code that runs before each test
            //begin a new transaction
            t = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //code that runs after each test
            //Rollback the transaction by calling dispose
            t.Dispose();

        }

        [TestMethod]
        public void ShowCampsitesAvailableTest()
        {
            int parkId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO park VALUES ('fake park', 'fake location', '9/9/9999', 99, 99, 'fake description'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);

                parkId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            int campgroundId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO campground VALUES ({parkId}, 'fake campground', '1', '2', '99'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);

                campgroundId = Convert.ToInt32(cmd.ExecuteScalar());

            }

            int siteId;
            DateTime requestedStart = DateTime.Today;
            DateTime requestedEnd = requestedStart.AddDays(5);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO site VALUES ({campgroundId}, '1', '99', '0', '99', '55', '0'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);

                siteId = Convert.ToInt32(cmd.ExecuteScalar());

            }

            CampsiteDAL dal = new CampsiteDAL(connectionString);
            List<int> campsites = dal.ShowCampsitesAvailable(campgroundId, requestedStart, requestedEnd);
            Assert.AreEqual(1, campsites.Count);
            Assert.AreEqual(siteId, campsites[0]);
        }
    }
}
