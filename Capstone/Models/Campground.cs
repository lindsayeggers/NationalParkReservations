using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
   public class Campground
    {
        public int Campground_id;
        public int Park_id;
        public string Name;
        public int Open_from_mm;
        public int Open_to_mm;
        public decimal Daily_fee;
    }
}
