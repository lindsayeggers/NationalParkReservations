using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.CLI;
using Capstone.Models;
using System.Data.SqlClient;
using Capstone.DAL;

namespace Capstone.CLI
{
    public class OtherCLI
    {        
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Search all available campsites within a park");
                Console.WriteLine("2 - Show all reservations in the next 30 days");
                Console.WriteLine("Q - Return to previous menu");
                Console.WriteLine();

                string input = CLIHelper.GetString("Please select an option:");
                Console.WriteLine();
                input = input.ToUpper();
                if (input == "1" || input == "2" || input == "Q")
                {
                    switch (input)
                    {
                        case "1":
                            DisplayCampgroundsAvailable();
                            break;

                        case "2":
                            CurrentReservations();
                            break;
                        
                        case "Q":
                            break;
                    }
                }
                else
                {
                    Console.Beep(1700, 250);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Character entered is not a 1, 2, or Q");
                    Console.ResetColor();
                }
            }
        }
        

        public void CurrentReservations()
        {
            OtherDAL other = new OtherDAL();
            int parkNum = CLIHelper.GetInteger("Enter Park to see all available sites");
            other.AllReservations30Days(parkNum);
        }

        public void DisplayCampgroundsAvailable()
        {
            OtherDAL other = new OtherDAL();
            int parkNum = CLIHelper.GetInteger("Enter Park to see all reservations");
            DateTime start = CLIHelper.GetDateTime("Arrival Date");
            DateTime end = CLIHelper.GetDateTime("Departure Date");
            other.ParkCampsitesAvailable(parkNum, start, end);
        }
        
    }
}
        