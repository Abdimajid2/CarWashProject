using Backend.API.Data;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Services
{
    public class UpdateTimeslots : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateTimeslots(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    await MaintainTimeSlots(context);

                    // Check every 6 hours
                    await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
                }
                catch
                {
                    // If error, wait 1 hour and try again
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }

        public async Task MaintainTimeSlots(AppDbContext context)
        {
            var today = DateTime.UtcNow.Date;
            var sevenDaysAhead = today.AddDays(7);

            //Remove old slots (before today)
            var oldSlots = context.TimeSlots.Where(ts => ts.AppointmentDate < today);
            context.TimeSlots.RemoveRange(oldSlots);

            //Add missing slots for next 7 days
            for (var date = today; date < sevenDaysAhead; date = date.AddDays(1))
            {
                // Check if this date already has slots
                var hasSlots = await context.TimeSlots
                    .AnyAsync(ts => ts.AppointmentDate.Date == date.Date);

                if (!hasSlots)
                {
                    // Create 8 hourly slots (9 AM to 5 PM)
                    for (int hour = 9; hour < 17; hour++)
                    {
                        context.TimeSlots.Add(new TimeSlot
                        {
                            StartTime = new TimeSpan(hour, 0, 0),
                            EndTime = new TimeSpan(hour + 1, 0, 0),
                            AppointmentDate = date,
                            IsAvailable = true
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
    
