using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1TestEF.Models.DbContextAndRepository
{
    public sealed class WebApplication1TestEFDbContext : DbContext
    {
        public DbSet<MetricAgent> MetricAgents { get; set; }

        public WebApplication1TestEFDbContext(DbContextOptions<WebApplication1TestEFDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MetricAgent>().HasAlternateKey(agent => agent.AddressAgent);
            modelBuilder.Entity<MetricAgent>().Property(agent => agent.AddressAgent).IsRequired();
        }
    }
}
