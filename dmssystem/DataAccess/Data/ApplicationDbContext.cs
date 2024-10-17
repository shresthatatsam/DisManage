using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Victim> Victims { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Disaster> Disasters { get; set; }
        public DbSet<Image> DisasterImages { get; set; }

        public DbSet<DisasterType> disasterTypes  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Victim>()
             .HasOne(v => v.Location)
             .WithOne(l => l.Victim)
             .HasForeignKey<Location>(l => l.VictimId);

            // 1-to-1 relationship: Victim has 1 Disaster
            builder.Entity<Victim>()
                .HasOne(v => v.Disaster)
                .WithOne(d => d.Victim)
                .HasForeignKey<Disaster>(d => d.VictimId);

            builder.Entity<Disaster>()
          .HasMany(d => d.Images)
          .WithOne(di => di.Disaster)
          .HasForeignKey(di => di.DisasterId)
          .OnDelete(DeleteBehavior.Cascade); // Cascade delete for Disaster-DisasterImage

            // Disable Cascade Delete on DisasterImages -> Victim to prevent multiple cascade paths
            builder.Entity<Image>()
                .HasOne(di => di.Victim)
                .WithMany(v => v.Images)
                .HasForeignKey(di => di.VictimId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



