using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationSubMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";

        private string SQL_Reservation = @"insert into reservation values(@siteID, @name, @fromDate, @toDate, @createDate)";

        public void MakeReservation(int selection, DateTime start, DateTime end)
        {
            string reservationName = CLIHelper.GetString("Enter the name you are reserving this under");
            DateTime createdOn = DateTime.Now;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //int @campID = campgroundId;

                    SqlCommand cmd = new SqlCommand(SQL_Reservation, conn);
                    cmd.Parameters.AddWithValue("@siteID", selection);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@fromDate", start);
                    cmd.Parameters.AddWithValue("@toDate", end);
                    cmd.Parameters.AddWithValue("@createDate", createdOn);

                    SqlDataReader reader = cmd.ExecuteReader();
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Thanks for reserving your campsite with Unicorn Park Registry");
            }
        }
    }


}
