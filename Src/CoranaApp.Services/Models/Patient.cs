using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoronaApp.Services
{
    public class Patient
    {
        public Patient()
        {
            Path = new List<Location>();
        }
        [Key]
        public int Id { get; set; }
        public int Age { get; set; }

        public int Password { get; set; }
        public string Name { get; set; }

        public string Token { get; set; }
        public List<Location> Path { get; set; }


    }
}