using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SignalRDemo.Data
{
    public class JobContextFactory : IDesignTimeDbContextFactory<JobContext>
    {
        public JobContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}SignalRDemo.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new JobContext(config.GetConnectionString("ConStr"));
        }
    }

    public class JobContext : DbContext
    {
        private string _connectionString;
        public JobContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Working> Workings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(_connectionString));
        }
    }
}
