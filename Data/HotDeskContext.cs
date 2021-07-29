using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotDesk.Models;

namespace HotDesk.Data
{
    public class HotDeskContext : DbContext
    {
        public HotDeskContext (DbContextOptions<HotDeskContext> options)
            : base(options)
        {
        }

        public DbSet<HotDesk.Models.ResvDate> ResvDate { get; set; }
        public DbSet<HotDesk.Models.AvailDesk> AvailDesk { get; set; }
        public DbSet<HotDesk.Models.Desk> Desk { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResvDate>().ToTable("ResvDate");
            modelBuilder.Entity<AvailDesk>().ToTable("AvailDesk");
            modelBuilder.Entity<Desk>().ToTable("Desk");
        }
    }
}
