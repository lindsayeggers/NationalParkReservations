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
    public class CampgroundDALTests
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
        public void GetAllCampgroundsTest()
        {
            int parkId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO park VALUES ('fake park', 'fake location', '9/9/9999', 99, 99, 'fake description'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);

                parkId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            int campgroundId;
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO campground VALUES ({parkId}, 'fake campground', '1', '2', '99'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
             
                campgroundId = Convert.ToInt32(cmd.ExecuteScalar());

            }

            CampgroundDAL dal = new CampgroundDAL(connectionString);
            List<Campground> campground = dal.GetAllCampgrounds(parkId);
            Assert.AreEqual(1, campground.Count); 
            Assert.AreEqual(campgroundId, campground[0].Campground_id);
            

        }
    }
}
