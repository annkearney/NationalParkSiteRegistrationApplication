using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Park
    {
        public int park_ID { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public DateTime establish_date { get; set; }

        public int area { get; set; }

        public int visitors { get; set; }

        public string desctription { get; set; }

        public override string ToString()
        {
            return name.PadRight(15);
        }
    }
}
