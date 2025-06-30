using Backend.API.Data;
using Backend.API.Models;

namespace Backend.API.Seeds
{
    public class SeedDb
    {
        public static void seedDb(AppDbContext context)
        {
            //ensure that db is created
            context.Database.EnsureCreated();

            var serviceTypes = new ServiceTypes[]
            {
            new ServiceTypes { Name = "Basic Wash", Description = "Exterior wash and dry", Price = 15.99m },
            new ServiceTypes { Name = "Premium Wash", Description = "Exterior + interior vacuum", Price = 24.99m },
            new ServiceTypes { Name = "Deluxe Wash", Description = "Full service with wax", Price = 39.99m },
            new ServiceTypes { Name = "Express Wash", Description = "Quick exterior wash", Price = 9.99m }
            };
            context.Servitypes.AddRange(serviceTypes);
            context.SaveChanges();

            var timeSlots = new List<TimeSlot>();
            var startDate = DateTime.Today;

            for (int day = 0; day < 7; day++)
            {
                var currentDate = startDate.AddDays(day);

                // Create hourly slots from 9 AM to 5 PM
                for (int hour = 9; hour < 17; hour++)
                {
                    timeSlots.Add(new TimeSlot
                    {
                        StartTime = new TimeSpan(hour, 0, 0),
                        EndTime = new TimeSpan(hour + 1, 0, 0),
                        AppointmentDate = currentDate,
                        IsAvailable = true
                    });
                }
            }

            context.TimeSlots.AddRange(timeSlots);
            context.SaveChanges();

        }
    }
}
