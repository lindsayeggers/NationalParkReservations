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
    public class CampgroundSubMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";
        
        public void DisplayCampGroundMenu()
        {
            while (true)
            {
                CampgroundSubMenu cgsm = new CampgroundSubMenu();

                int passToDisplayReservationMenu = cgsm.ShowAllCampgrounds();

                Console.WriteLine();
                Console.WriteLine("1 - Search for available reservation");
                Console.WriteLine("Q - return to previous menu");
                Console.WriteLine();

                string input = CLIHelper.GetString("Please select an option:");
                Console.WriteLine();
                input = input.ToUpper();
                if (input == "1" || input == "Q")
                {

                    switch (input)
                    {
                        case "1":
                            //leads to sub menu
                            CampgroundSubMenu camp = new CampgroundSubMenu();
                            camp.DisplayReservationMenu(passToDisplayReservationMenu);
                            break;

                        case "Q":
                            CapstoneCLI mainDisplay = new CapstoneCLI();
                            mainDisplay.Run();
                            return;
                    }
                }
                else
                {
                    Console.Beep(1700, 250);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Character entered is not a 1 or Q");
                    Console.ResetColor();
                }
            }
        }

        public int ShowAllCampgrounds()
        {
            int idInput;
            while (true)
            {
                ParksDAL dal = new ParksDAL(connectionString);
                List<Park> allParks = dal.GetAllParks();
                Console.WriteLine("-------PARKS-------");

                foreach (Park p in allParks)
                {
                    DateTime dateOnly = p.Establish_Date;

                    Console.WriteLine($"Park Id: {p.Id}) Name: {p.Name}  Location: {p.Location}");
                }

                idInput = CLIHelper.GetInteger("Please select park (enter park id): ");
                Console.WriteLine();
                if (idInput == 1 || idInput == 2 || idInput == 3)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("invalid parkID");
                    Console.ResetColor();
                }
            }


            CampgroundDAL cdal = new CampgroundDAL(connectionString);
            List<Campground> allCampgrounds = cdal.GetAllCampgrounds(idInput);

            Console.WriteLine("------CAMPGROUNDS------");
            foreach (Campground c in allCampgrounds)
            {

                decimal dailyFee = c.Daily_fee;

                Console.WriteLine($"Id: {c.Campground_id} Name: {c.Name} Open from: {c.Open_from_mm} - {c.Open_to_mm} Fee: {dailyFee.ToString("c")}");
            }
            return idInput;
        }

        public void DisplayReservationMenu(int park)
        {
            CampsiteSubMenu siteMenu = new CampsiteSubMenu();
            siteMenu.SearchAvailableDates(park);
        }
    }    
}
    

