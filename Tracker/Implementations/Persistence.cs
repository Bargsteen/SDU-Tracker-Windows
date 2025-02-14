using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Implementations
{
    public class Persistence : IPersistence
    {
        private readonly DbContextOptions<UsageContext> _options;

        public Persistence()
        {
            string dbDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SDU", "Tracker");

            // Ensure that the directories are created, since EF can't handle that.
            Directory.CreateDirectory(dbDirectoryPath);

            string dbFilePath = Path.Combine(dbDirectoryPath, "sdu-tracker.db");
            
            _options = new DbContextOptionsBuilder<UsageContext>().UseSqlite($"DataSource = {dbFilePath}").Options;

            EnsureDatabaseIsCreated();
        }

        // Used for testing
        public Persistence(DbConnection connection)
        {
            _options = new DbContextOptionsBuilder<UsageContext>()
                .UseSqlite(connection)
                .Options;
            EnsureDatabaseIsCreated();
        }

        private void EnsureDatabaseIsCreated()
        {
            using (var db = new UsageContext(_options))
            {
                db.Database.EnsureCreated();
            }
        }

        public void Save<T>(T usage) where T : Usage
        {
            using (var db = new UsageContext(_options))
            {
                db.Add(usage);
                db.SaveChanges();
            }
        }

        public void Delete<T>(T usage) where T : Usage
        {
            using (var db = new UsageContext(_options))
            {
                db.Remove(usage);
                db.SaveChanges();
            }
        }

        public List<AppUsage> FetchAppUsages(int upTo)
        {
            using (var db = new UsageContext(_options))
            {
                return db.AppUsages.OrderBy(a => a.TimeStamp).Take(upTo).ToList();
            }
        }

        public List<DeviceUsage> FetchDeviceUsages(int upTo)
        {
            using (var db = new UsageContext(_options))
            {
                return db.DeviceUsages.OrderBy(a => a.TimeStamp).Take(upTo).ToList();
            }
        }
    }


    public class UsageContext : DbContext
    {
        public DbSet<AppUsage> AppUsages { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }

        public UsageContext(DbContextOptions options) : base(options)
        {
        }
    }
}