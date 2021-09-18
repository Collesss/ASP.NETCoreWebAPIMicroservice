using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMetricsManager
{
    public sealed class ManagerDbContext : DbContext
    {
        public DbSet<MetricAgent> MetricAgents { get; set; }

        public ManagerDbContext(DbContextOptions<ManagerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MetricAgent>().HasAlternateKey(agent => agent.AddressAgent);
            modelBuilder.Entity<MetricAgent>().Property(agent => agent.AddressAgent).IsRequired();
        }
    }
}
