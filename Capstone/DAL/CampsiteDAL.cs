using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.CLI;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampsiteDAL
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";

        private const string SQL_CurrentReservations = "select * from reservation join site on site.site_id = reservation.site_id join campground on site.campground_id = campground.campground_id where campground.campground_id = @emptyCampsite";
        private const string SQL_UnreservedSites = "select * from site where site.site_id not in (select reservation.site_id from reservation) and site.campground_id = @emptyCampsite;";
        private const string SQL_CheckCorrectPark = "select campground.campground_id from campground where campground.park_id = @parkID";

        public CampsiteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
                
        public List<int> ShowCampsitesAvailable(int availableSite, DateTime requestedStart, DateTime requestedEnd)
        {
            Dictionary<int, bool> openCampsites = new Dictionary<int, bool>();
            List<int> siteOpen = new List<int>();
            //for site that has no current reservations
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();



                    //int @emptyCampsite = availableSite;

                    SqlCommand cmd = new SqlCommand(SQL_UnreservedSites, conn);
                    cmd.Parameters.AddWithValue("@emptyCampsite", availableSite);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int siteID = Convert.ToInt32(reader["site_id"]);
                        bool isAvailable = true;
                        openCampsites.Add(siteID, isAvailable);
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //int @emptyCampsite = availableSite;

                    SqlCommand cmd = new SqlCommand(SQL_CurrentReservations, conn);
                    cmd.Parameters.AddWithValue("@emptyCampsite", availableSite);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int siteID = Convert.ToInt32(reader["site_id"]);
                        DateTime existingStart = Convert.ToDateTime(reader["from_date"]);
                        DateTime existingEnd = Convert.ToDateTime(reader["to_date"]);
                        bool isAvailable;

                        if ((requestedStart >= existingStart && requestedStart < existingEnd) && (requestedEnd <= existingEnd && requestedEnd > existingStart))
                        {
                            isAvailable = false;
                            openCampsites[siteID] = isAvailable;
                        }
                        else
                        {
                            if (requestedStart < existingStart && requestedEnd <= existingStart)
                            {
                                isAvailable = true;
                                if (!openCampsites.ContainsKey(siteID))
                                {
                                    openCampsites.Add(siteID, isAvailable);
                                }
                                else if (openCampsites.ContainsKey(siteID) && openCampsites[siteID] == true)
                                {
                                    openCampsites[siteID] = isAvailable;
                                }
                            }
                            else if (requestedStart >= existingEnd && requestedEnd > existingEnd)
                            {
                                isAvailable = true;
                                if (!openCampsites.ContainsKey(siteID))
                                {
                                    openCampsites.Add(siteID, isAvailable);
                                }
                                else if (openCampsites.ContainsKey(siteID) && openCampsites[siteID] == true)
                                {
                                    openCampsites[siteID] = isAvailable;
                                }
                            }
                            else
                            {
                                isAvailable = false;
                                openCampsites[siteID] = isAvailable;
                            }


                        }
                    }
                                        
                    foreach(KeyValuePair<int, bool> x in openCampsites)
                    {                        
                        if(x.Value == true)
                        {
                            siteOpen.Add(x.Key);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return siteOpen;
        }

        public void ConfirmPark(int campgroundIdInput, int park)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                while (true)
                {
                    SqlCommand cmd = new SqlCommand(SQL_CheckCorrectPark, conn);
                    cmd.Parameters.AddWithValue("@parkID", park);

                    SqlDataReader sr = cmd.ExecuteReader();
                    List<int> availableParks = new List<int>();

                    while (sr.Read())
                    {
                        int campground_id;
                        campground_id = Convert.ToInt32(sr["campground_id"]);
                        availableParks.Add(campground_id);
                    }

                    if (availableParks.Contains(campgroundIdInput))
                    {
                        return;
                    }
                    else
                    {
                        availableParks.Clear();
                        Console.Beep(1700, 250);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Campground entry");
                        Console.ResetColor();
                        CampsiteSubMenu cssm = new CampsiteSubMenu();
                        cssm.SearchAvailableDates(park);
                    }

                }

            }
        }
    }
}
