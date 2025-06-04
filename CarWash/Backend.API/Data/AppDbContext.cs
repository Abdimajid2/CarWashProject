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
        public DbSet<ServiceType> Servitypes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
    }
}
