using System.Collections.Generic;

namespace CoronaApp.Dal
{
    public class Patient
    {
        public Patient()
        {
            Path = new List<Location>();
        }

        public int Id { get; set; }
        public int Age { get; set; }
        public List<Location> Path { get; set; }
    }
}