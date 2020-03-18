using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservation_id { get; set; }

        public int site_id { get; set; }

        public string name { get; set; }

        public DateTime from_date { get; set; }

        public DateTime to_date { get; set; }

        public DateTime create_date { get; set; }
        //Datetime correct varibale?

        public override string ToString()
        {
            return name.PadRight(15);
        }
    }
}
