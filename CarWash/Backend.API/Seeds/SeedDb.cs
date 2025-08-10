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

            //delete existing data
            context.ServiceTypes.RemoveRange(context.ServiceTypes);
            context.TimeSlots.RemoveRange(context.TimeSlots);
            context.SaveChanges();

            var serviceTypes = new ServiceTypes[]
            {
             new ServiceTypes { Name = "Grundtvätt", Description = "Utvändig tvätt och torkning", Price = 459 },
             new ServiceTypes { Name = "Premiumtvätt", Description = "Utvändig tvätt + invändig dammsugning", Price = 850 },
             new ServiceTypes { Name = "Deluxetvätt", Description = "Fullservice med vax", Price = 1500 },
            new ServiceTypes { Name = "Snabbtvätt", Description = "Snabb utvändig tvätt", Price = 350 }
            };
            context.ServiceTypes.AddRange(serviceTypes);
            context.SaveChanges();

            var timeSlots = new List<TimeSlot>();
            var startDate = DateTime.UtcNow.Date;

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
