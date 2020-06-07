//using CoronaApp.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaApp.Services
{
    public class CoronaContext : DbContext
    {
        public CoronaContext()

        {
        }
        public CoronaContext(DbContextOptions<CoronaContext> options)
          : base(options)
        {

        }

        public DbSet<Location> Location { get; set; }
        public DbSet<Patient> Patient { get; set; }
        //public DbSet<Log> Log { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ILRLEMBERGERLT; Database= CoronaDB; Trusted_Connection = True;");
                //optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\RLemberger\\Documents\\Brix\\Rina&Rachelly\\CoronaProject\\DB\\CoronaDB.mdf;Integrated Security=True;Connect Timeout=30");
                //optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\PersonCorona; AttachDbFilename=C:\Users\RLemberger\Documents\Brix\Rina&Rachelly\CoronaProject\DB\CoronaDB.mdf; Integrated Security=True; Connect Timeout=30");
                //optionsBuilder.UseSqlServer(@"Server=ILRLEMBERGERLT;Database=PersonCorona;Trusted_Connection=true;");
            }
        }

    }
}
