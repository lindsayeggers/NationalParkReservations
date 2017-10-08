using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DeliveryProviders;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationCLI
    {    
        private IDeliveryService deliveryService;

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";
        private const string reservationNum = "select max(reservation.reservation_id) as 'conf_number' from reservation";

        public ReservationCLI(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }


        public void Run(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(reservationNum, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int confirmationNumber = Convert.ToInt32(reader["conf_number"]);

                string recipient = name;
                string message = $"Confirmation Number is {confirmationNumber}";

                this.deliveryService.Send(recipient, message);
                
                Console.ReadLine();
            }
        }

    }
}






    //date to day conversion in order to prevent reservation overlap
    //DateTime it = new DateTime();

    //Console.WriteLine("hey");
    //string b = Console.ReadLine();
    //it = DateTime.Parse(b);
    //int day = it.DayOfYear;

    //Console.WriteLine(day);

    //DateTime date = DateTime.Now;
    //int day = date.DayOfYear;
    //Console.Write(day);

