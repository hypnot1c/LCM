using LCM.Data.Master.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCM.Data.Master.EF
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<LoyaltyCardModel> LoyaltyCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //  string databaseFilePath = "iVM.Vehicle.db";
            //  try
            //  {
            //    databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            //  }
            //  catch (InvalidOperationException)
            //  { }

            //  optionsBuilder.UseSqlite($"Data source={dat abaseFilePath}");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<VehicleModel>(VehicleModelConfiguration.Configure);
            //modelBuilder.Entity<EventOccuredModel>(EventOccuredModelConfiguration.Configure);
        }
    }
}
