using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ServiceTypes> Servitypes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TimeSlot)
                .WithOne()
                .HasForeignKey<Booking>(b => b.TimeSlotId);
        }
    }
}
