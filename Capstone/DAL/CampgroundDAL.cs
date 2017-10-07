using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connectionString;

        public CampgroundDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> GetAllCampgrounds(int parkInput)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //campground.
                    //int @id = parkInput;
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE campground.park_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", parkInput);
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.Campground_id = Convert.ToInt32(reader["campground_id"]);
                        c.Name = Convert.ToString(reader["name"]);
                        c.Open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
                        c.Open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
                        c.Daily_fee = Convert.ToDecimal(reader["daily_fee"]);

                        output.Add(c);                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;

            }
            return output;
        }
    }
}
