using System;
using System.ComponentModel.DataAnnotations;

namespace CoronaApp.Services
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string LocationC { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PatientId { get; set; }
    }
}