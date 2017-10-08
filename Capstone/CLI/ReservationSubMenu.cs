using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;
using Capstone.DeliveryProviders;
using Capstone.CLI;

namespace Capstone.DAL
{
    public class ReservationSubMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";

        private string SQL_Reservation = @"insert into reservation values(@siteID, @name, @fromDate, @toDate, @createDate)";

        public void MakeReservation(int selection, DateTime start, DateTime end)
        {
            string reservationName = CLIHelper.GetString("Enter the name for the reservation:");
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

                    while (true)
                    {
                        Console.WriteLine("Welcome to our delivery service program.");
                        Console.WriteLine("Please choose one of the following delivery options.");
                        Console.WriteLine("1 - Console Delivery");
                        Console.WriteLine("2 - Email Delivery");
                        //Console.WriteLine("3 - SMS Delivery");
                        Console.WriteLine();
                        string runInput ="";
                        string choice = CLIHelper.GetString("Make a choice > ");


                        IDeliveryService ds = null;
                        switch (choice)
                        {
                            case "1":
                                ds = new ConsoleDeliveryService();
                                Console.WriteLine();
                                runInput = reservationName;
                                break;

                            case "2":
                                ds = new EmailDeliveryService();
                                Console.WriteLine("please enter your email address");
                                runInput = Console.ReadLine();
                                break;

                                //case "3":
                                //    ds = new SMSDeliveryService();
                                //    break;
                        }
                        ReservationCLI confirmation = new ReservationCLI(ds);

                        confirmation.Run(runInput);
                        break;
                    }
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
