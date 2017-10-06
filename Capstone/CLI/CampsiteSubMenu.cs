using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.CLI
{
    class CampsiteSubMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";

        private const string SQL_Fee = "select campground.daily_fee from campground where campground.campground_id = @campID";

        public void SearchAvailableDates()
        {
            //add campsite number
            int campgroundId = CLIHelper.GetInteger("Please enter the campground you prefer");
            //add start date
            DateTime start = CLIHelper.GetDateTime("please enter desired start date MM/DD/YYYY");
            //add end date
            DateTime end = CLIHelper.GetDateTime("please enter desired end date MM/DD/YYYY");

            CampsiteDAL camp = new CampsiteDAL(connectionString);
            List<int> campArea = camp.ShowCampsitesAvailable(campgroundId, start, end);
            int i = 0;
            Campground selection = new Campground();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //int @campID = campgroundId;

                SqlCommand cmd = new SqlCommand(SQL_Fee, conn);
                cmd.Parameters.AddWithValue("@campID", campgroundId);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                decimal fee = Convert.ToDecimal(reader["daily_fee"]);
                int dateDifference = Convert.ToInt32((end - start).TotalDays);
                string totalFee = (dateDifference * fee).ToString("c");
                Console.WriteLine("\n---Available Sites---");

                while (i != campArea.Count && i < 5)
                {
                    Console.WriteLine($"site: {campArea[i]}, total cost: {totalFee}");
                    i++;
                }

                Console.WriteLine();
                
            }

            Console.WriteLine("1 - Make a reservation");
            Console.WriteLine("Q - return to previous menu");
            Console.WriteLine();

            string input = CLIHelper.GetString("Please select an option:");

            ReservationSubMenu reserveSpot = new ReservationSubMenu();

            switch (input.ToUpper())
            {
                case "1":
                    int site = CLIHelper.GetInteger("please select the site you'd like to reserve");
                    reserveSpot.MakeReservation(site, start, end);
                    break;

                case "Q":
                    CampgroundSubMenu campDisplay = new CampgroundSubMenu();
                    campDisplay.DisplayCampGroundMenu();
                    break;

            }
            return;

        }
    }
}
