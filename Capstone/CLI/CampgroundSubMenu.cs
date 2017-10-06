using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.CLI
{
    public class CampgroundSubMenu
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";


        public void DisplayCampGroundMenu()
        {
            CampgroundSubMenu cgsm = new CampgroundSubMenu();

            cgsm.ShowAllCampgrounds();

            Console.WriteLine("1 - Search for available reservation");
            Console.WriteLine("Q - return to previous menu");
            Console.WriteLine();

            string input = CLIHelper.GetString("Please select an option");

            switch (input.ToUpper())
            {
                case "1":
                    //leads to sub menu
                    CampgroundSubMenu camp = new CampgroundSubMenu();
                    camp.DisplayReservationMenu();
                    break;

                case "Q":
                    CapstoneCLI mainDisplay = new CapstoneCLI();
                    mainDisplay.Run();
                    break;
                    return;
            }
        }

        public void ShowAllCampgrounds()
        {
            ParksDAL dal = new ParksDAL(connectionString);
            List<Park> allParks = dal.GetAllParks();

            foreach (Park p in allParks)
            {
                DateTime dateOnly = p.Establish_Date;

                Console.WriteLine($"Park Id: {p.Id}) Name: {p.Name}  Location: {p.Location}");
            }

            int idInput = CLIHelper.GetInteger("Please select park (enter park id): ");
            CampgroundDAL cdal = new CampgroundDAL(connectionString);
            List<Campground> allCampgrounds = cdal.GetAllCampgrounds(idInput);

            foreach (Campground c in allCampgrounds)
            {

                decimal dailyFee = c.Daily_fee;

                Console.WriteLine($"Id: {c.Campground_id} Name: {c.Name} Open from: {c.Open_from_mm} - {c.Open_to_mm} Fee: {dailyFee.ToString("c")}");
            }                     
        }

        public void DisplayReservationMenu()
        {
            CampsiteSubMenu siteMenu = new CampsiteSubMenu();
            siteMenu.SearchAvailableDates();
        }
    }
    
}
