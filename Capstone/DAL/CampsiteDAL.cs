﻿using System;
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
        private string connectionString;

        public CampsiteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ShowCampsitesAvailable()
        {
            List<Campsite> output = new List<Campsite>();


            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();

            //        //int @id = parkInput;
            //        SqlCommand cmd = new SqlCommand("SELECT campground.* FROM campground WHERE campground.park_id = @id", conn);
            //        cmd.Parameters.AddWithValue("@id", parkInput);

            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            Campground c = new Campground();
            //            c.Campground_id = Convert.ToInt32(reader["campground_id"]);
            //            c.Name = Convert.ToString(reader["name"]);
            //            c.Open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
            //            c.Open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
            //            c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);

            //            output.Add(c);
            //        }
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
        }
    }
}