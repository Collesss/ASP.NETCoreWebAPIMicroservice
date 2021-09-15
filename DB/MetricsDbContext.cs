using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB
{
    public sealed class MetricsDbContext : DbContext
    {
        public DbSet<CpuMetric> CpuMetrics { get; set; }
        public DbSet<HardDriveMetric> HardDriveMetrics { get; set; }
        public DbSet<NetMetric> NetMetrics { get; set; }
        public DbSet<NetworkMetric> NetworkMetrics { get; set; }
        public DbSet<RamMetric> RamMetrics { get; set; }

        public MetricsDbContext(DbContextOptions<MetricsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
