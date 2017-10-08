using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone.CLI;

namespace Capstone
{
    public class CapstoneCLI
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";


        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Show all parks");
                Console.WriteLine("2 - Show all campgrounds");
                Console.WriteLine("3 - Other Menu options");
                Console.WriteLine("Q - Quit");
                Console.WriteLine();

                string input = CLIHelper.GetString("Please select an option:");
                Console.WriteLine();
                input = input.ToUpper();
                if (input == "1" || input == "2" || input == "3" || input == "Q")
                {
                    switch (input)
                    {
                        case "1":
                            ShowAllParks();
                            break;

                        case "2":
                            //leads to sub menu
                            CampgroundSubMenu camp = new CampgroundSubMenu();
                            camp.DisplayCampGroundMenu();
                            break;

                        case "3":
                            OtherCLI other = new OtherCLI();
                            other.Menu();
                            break;

                        case "Q":
                            Console.WriteLine("Thanks for visiting Unicorn National Parks Registry");
                            Console.ReadLine();
                            return;
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

        private void ShowAllParks()
        {
            ParksDAL dal = new ParksDAL(connectionString);
            List<Park> allParks = dal.GetAllParks();

            foreach (Park p in allParks)
            {
                DateTime dateOnly = p.Establish_Date;

                Console.WriteLine();
                Console.WriteLine($"{p.Id}) {p.Name}  {p.Location} \tEstablished in - {dateOnly.ToString("d")} \tVisitors - {p.Visitors}");
                Console.WriteLine();
                Console.WriteLine($"DESCRIPTION: {p.Description}");


            }
        }
        //private void ShowAllCampgrounds()
        //{
        //    ParksDAL dal = new ParksDAL(connectionString);
        //    List<Park> allParks = dal.GetAllParks();

        //    foreach (Park p in allParks)
        //    {
        //        DateTime dateOnly = p.Establish_Date;

        //        Console.WriteLine($"Park Id: {p.Id}) Name: {p.Name}  Location: {p.Location}");
        //    }

        //    int idInput = CLIHelper.GetInteger("Please select park (enter park id): ");
        //    CampgroundDAL cdal = new CampgroundDAL(connectionString);
        //    List<Campground> allCampgrounds = cdal.GetAllCampgrounds(idInput);

        //    foreach(Campground c in allCampgrounds)
        //    {

        //        decimal dailyFee = c.Daily_fee;

        //        Console.WriteLine();
        //        Console.WriteLine($"Id: {c.Campground_id} Name: {c.Name} Open from: {c.Open_from_mm} - {c.Open_to_mm} Fee: {dailyFee.ToString("c")}");

        //    }
        //}
    }
}