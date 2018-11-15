using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TrackerLib.Interfaces;
using TrackerLib.Models;


namespace TrackerLib.Implementations
{
    public class Persistence : IPersistence
    {
        private readonly bool _databaseType;
        private readonly string _connectionString;

        

        public Persistence(bool useInMemoryDatabase = false)
        {
            _databaseType = useInMemoryDatabase;
            _connectionString = _databaseType ? "DataSource = :memory:" : "DataSource = sdu-tracker.db";

            using (var db = GetUsageContext())
            {
                db.Database.Migrate();
            }
        }

        public void Save<T>(T usage) where T : Usage
        {
            using (var db = GetUsageContext())
            {
                db.Add(usage);
                db.SaveChanges();
            }
        }

        public void Delete<T>(T usage) where T : Usage
        {
            using (var db = GetUsageContext())
            {
                db.Remove(usage);
                db.SaveChanges();
            }
        }

        public List<AppUsage> FetchAppUsages(int upTo)
        {
            using (var db = GetUsageContext())
            {
                return db.AppUsages.OrderBy(a => a.TimeStamp).Take(upTo).ToList();
            }
        }

        public List<DeviceUsage> FetchDeviceUsages(int upTo)
        {
            using (var db = GetUsageContext())
            {
                return db.DeviceUsages.OrderBy(a => a.TimeStamp).Take(upTo).ToList();
            }
        }

        private UsageContext GetUsageContext()
        {
            var connection = new SqliteConnection(_connectionString);
            var options = new DbContextOptionsBuilder<UsageContext>()
                .UseSqlite(connection)
                .Options;
            return new UsageContext(options);
        }
    }


    public class UsageContext : DbContext
    {
        public DbSet<AppUsage> AppUsages { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

        public UsageContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource = sdu-tracker.db");
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<UsageContext>
    {
        public UsageContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsageContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");

            return new UsageContext(optionsBuilder.Options);
        }
    }
}