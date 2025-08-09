using Backend.API.Data;
using Backend.API.Models;
using Shared.Models.Enum;
using System.Diagnostics;

namespace Backend.API.Services
{
    public class BookingServices
    {
        private readonly AppDbContext _context;

        public BookingServices(AppDbContext context)
        {
            _context = context; 

        }
        public async Task<Booking?> CreateBookingAsync(string LicensePlate, string email, int serviceTypeId, int timeSlotId)
        {
            try
            {
                //check if timeslot is available
                var timeslot = await _context.TimeSlots.FindAsync(timeSlotId);
                if (timeslot == null || timeslot.IsAvailable != true)
                {
                    return null;
                }

                //check if service type exist
                var serviceTypes = await _context.ServiceTypes.FindAsync(serviceTypeId);
                if (serviceTypes == null)
                {
                    return null;
                }

                //create new booking
                var newBooking = new Booking
                {
                    LicensePlate = LicensePlate.ToUpper(),
                    Email = email,
                    ServiceTypeId = serviceTypeId,
                    TimeSlotId = timeSlotId,
                    Status = BookingStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    IsEmailConfirmed = false,
                    ConfirmationToken = Guid.NewGuid().ToString(),
                };

                //add booking to db
                _context.Bookings.Add(newBooking);

                //mark booked timeslot unavailable
                timeslot.IsAvailable = false;

                await _context.SaveChangesAsync();

                return newBooking;

            }

            catch (Exception ex)
            {
                Debug.WriteLine("error creating booking:" , ex.Message);
                return null;
            }
        }
    }
}
