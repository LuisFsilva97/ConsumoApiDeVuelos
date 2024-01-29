using Microsoft.EntityFrameworkCore;
using Newshore.Viajes.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Repository.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            this.Database.EnsureCreated();
        }

        public DbSet<SearchHistory>? SearchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
