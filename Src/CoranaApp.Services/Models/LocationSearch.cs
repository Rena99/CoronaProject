using System;
using System.Collections.Generic;

namespace CoronaApp.Services
{
    public class LocationSearch
    {
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Age { get; set; }
    }
}