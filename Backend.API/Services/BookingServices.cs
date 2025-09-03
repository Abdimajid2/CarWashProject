using Backend.API.Data;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Enum;
using Shared.Models.ModelDTO;
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
        public async Task<Booking?> CreateBookingAsync(string licensePlate, string email, int serviceTypeId, int timeSlotId)
        {
            try
            {
                
                var timeslot = await _context.TimeSlots.FindAsync(timeSlotId);
                //check if timeslot is available
                if (timeslot == null || timeslot.IsAvailable != true)
                {
                    return null;
                }

                
                var serviceTypes = await _context.ServiceTypes.FindAsync(serviceTypeId);
                //check if service type exist
                if (serviceTypes == null)
                {
                    return null;
                }

                //create new booking
                var newBooking = new Booking
                {
                    LicensePlate = licensePlate.ToUpper(),
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


        public async Task<List<BookingDTO>> GettAllBookings()
        {
             var allabookings = await _context.Bookings
                .Include(b => b.ServiceType)
                .Include(b => b.TimeSlot)
                .OrderByDescending(a => a.CreatedAt)
                .Select(b => new BookingDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    Email = b.Email,
                    LicensePlate = b.LicensePlate.ToUpper(),
                    Status = b.Status,
                    ServiceType = new ServiceTypeDTO
                    {
                        Id = b.ServiceType.Id,
                        Name = b.ServiceType.Name,
                        Description = b.ServiceType.Description,
                        Price = b.ServiceType.Price
                    },
                    TimeSlot = new TimeSlotDTO
                    {
                        Id = b.TimeSlot.Id,
                        AppointmentDate = b.TimeSlot.AppointmentDate,
                        StartTime = b.TimeSlot.StartTime,
                        EndTime = b.TimeSlot.EndTime,
                        IsAvailable = b.TimeSlot.IsAvailable
                    }
                })
                .ToListAsync();

            return allabookings;
        }
    }
}
