using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class PetContext : DbContext
    {
        DbSet<PetDetails> petDetails { get; set; }
        DbSet<Cage> cages { get; set; }
        DbSet<FeedSchedule> FeedSchedules { get; set; }
        public string ConnectionString { get; }
        public PetContext ()
        {
            ConnectionString = "Server=ROMAN_PC\\SQLEXPRESS;Database=PetShop;User Id=imroman;Password=123456;Trusted_Connection=True;TrustServerCertificate=True";

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
