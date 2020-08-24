using System;
using System.Collections.Generic;
using System.Text;

namespace findU.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool IsOnline { get; set; }

        public string Message { get; set; }

        public string LastUpdatedTime { get; set; }


    }
}
