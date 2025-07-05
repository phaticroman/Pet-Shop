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
        public DbSet<PetDetails> petDetails { get; set; }
        public DbSet<Cage> cages { get; set; }
        public DbSet<FeedSchedule> FeedSchedules { get; set; }
        public DbSet<BuyingRecord> BuyingRecords { get; set; }
        public DbSet<SellingRecord> SellingRecords { get; set; }
        public int Id { get; set; }
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
