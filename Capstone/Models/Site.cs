using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int site_id { get; set; }

        public int campground_id { get; set; }

        public int site_number { get; set; }

        public int max_occupancy { get; set; }

        public bool accessible { get; set; }

        public int max_rv_length { get; set; }

        public bool utilites { get; set; }

        public string PrintID()
        {
            return site_id.ToString().PadRight(15);
        }

        public string PrintOccupancy()
        {
           
            return max_occupancy.ToString().PadRight(20);
            
        }

        public string PrintAccessible()
        {

            if (accessible)
            {
                return "Yes".PadRight(20);
            }
            else
            {
                return "No".PadRight(20);
            }

        }

        public string PrintRVLength()
        {
            if (max_rv_length == 0)
            {
                return "N/A".PadRight(20);

            }
            else
            {
                return max_rv_length.ToString().PadRight(15);
            }
               
        }

        public string PrintUtility()
        {

            if (utilites)
            {
                return "Yes".PadRight(15);
            }
            else
            {
                return "No".PadRight(15);
            }

        }




        



    }
}
