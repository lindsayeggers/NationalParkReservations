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
    public class OtherDAL
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Capstone;User ID=te_student;Password=sqlserver1";

        private const string SQL_CurrentReservations = "select * from reservation join site on site.site_id = reservation.site_id join campground on site.campground_id = campground.campground_id where campground.campground_id = @emptyCampsite";
        private const string SQL_UnreservedSites = "select * from site where site.site_id not in (select reservation.site_id from reservation) and site.campground_id = @emptyCampsite;";
        private const string SQL_ParkReservations = "select campground.* from campground where campground.park_id = @park";
        private const string SQL_AllParkReservations = "select reservation.* from reservation join site on reservation.site_id = site.site_id join campground on site.campground_id = campground.campground_id join park on campground.park_id = park.park_id where park.park_id = @park";

        public void ParkCampsitesAvailable(int parkID, DateTime requestedStart, DateTime requestedEnd)
        {
            Dictionary<int, bool> openCampsites = new Dictionary<int, bool>();
            List<Campground> parkCampgrounds = new List<Campground>();
            List<int> siteOpen = new List<int>();
            //for site that has no current reservations 

            List<int> CampIDs = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_ParkReservations, conn);
                    cmd.Parameters.AddWithValue("@park", parkID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground camp = new Campground();
                        camp.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        camp.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);
                        camp.Name = Convert.ToString(reader["name"]);
                        parkCampgrounds.Add(camp);

                        //int campNumber = Convert.ToInt32(reader["campground_id"]);
                        //CampIDs.Add(campNumber);
                    }

                }
            }
            catch
            {

            }
            
            for(int j = 0; j < parkCampgrounds.Count; j++)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();



                        //int @emptyCampsite = availableSite;

                        SqlCommand cmd = new SqlCommand(SQL_UnreservedSites, conn);
                        cmd.Parameters.AddWithValue("@emptyCampsite", parkCampgrounds[j].Campground_id);

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
                        cmd.Parameters.AddWithValue("@emptyCampsite", parkCampgrounds[j].Campground_id);

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

                        foreach (KeyValuePair<int, bool> x in openCampsites)
                        {
                            if (x.Value == true)
                            {
                                siteOpen.Add(x.Key);
                            }
                        }

                        int dateDifference = Convert.ToInt32((requestedEnd - requestedStart).TotalDays);
                        string totalFee = (dateDifference * parkCampgrounds[j].Daily_fee).ToString("c");
                        Console.WriteLine($"------Campground: {parkCampgrounds[j].Name}------");
                        int i = 0;
                        while (i != siteOpen.Count && i < 5)
                        {
                            Console.WriteLine($"Site: {siteOpen[i]}, Total Cost: {totalFee}");
                            i++;
                        }

                        Console.WriteLine();
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }               
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AllReservations30Days(int park)
        {
            List<Reservation> peopleStaying = new List<Reservation>();
            DateTime today = DateTime.Now;
            DateTime future = today.AddDays(30);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AllParkReservations, conn);
                    cmd.Parameters.AddWithValue("@park", park);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime dateStart = Convert.ToDateTime(reader["from_date"]);
                        DateTime dateEnd = Convert.ToDateTime(reader["to_date"]);

                        if (dateStart.Date >= today.Date && dateEnd.Date <= future.Date)
                        {
                            Reservation r = new Reservation();
                            r.Reservation_id = Convert.ToInt32(reader["reservation_id"]);
                            r.Site_id = Convert.ToInt32(reader["site_id"]);
                            r.Name = Convert.ToString(reader["name"]);
                            r.From_Date = Convert.ToDateTime(reader["from_date"]);
                            r.To_Date = Convert.ToDateTime(reader["to_date"]);
                            r.Create_Date = Convert.ToDateTime(reader["create_date"]);
                            peopleStaying.Add(r);
                        }
                    }
                }
            }
            catch
            {

            }
            foreach (Reservation r in peopleStaying)
            {
                Console.WriteLine($"Site: {r.Site_id} Dates booked:{r.From_Date} - {r.To_Date}");
            }
        }
    }
}

