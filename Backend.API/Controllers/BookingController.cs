using Backend.API.Data;
using Backend.API.Models;
using Backend.API.Requests;
using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models.ModelDTO;
using System.Diagnostics;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/Booking")]
    public class BookingController : Controller
    {
        private readonly ServiceTypesService _services;
        private readonly BookingServices _bookingServices;
        private readonly IServiceProvider _serviceProvider;

        public BookingController(ServiceTypesService services, BookingServices bookingServices, IServiceProvider serviceProvider)
        {
            _services = services;
            _bookingServices = bookingServices;
            _serviceProvider = serviceProvider;

             
        }

        [HttpGet("servicetypes")]
        public async Task<ActionResult<List<ServiceTypeDTO>>> GetAllServices()
        {
            try
            {
                var serviceTypes = await _services.GetAllServiceTypes();
                return Ok(serviceTypes);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"this went wrong {e.Message}" });
            }            
        }

        [HttpGet("timeslots")]
        public async Task<ActionResult<List<TimeSlotDTO>>> GetAllTimeSlots()
        {
            try
            {
                var timeslots = await _services.GetAllTimeSlots();
                return Ok(timeslots);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"this went wrong {e.Message}" });
                };
        }


        [HttpPost("createbooking")]
        public async Task<ActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            try
            {
                var booking = await _bookingServices.CreateBookingAsync(request.licensePlate, request.email,
                    request.serviceTypeId, request.timeSlotId);

                if(booking == null)
                {
                    return BadRequest(new { mewssage = "failed to create the booking" });
                }

                else
                {
                    return Ok(new { message = "Booking created successfully" });
                }
            }

            catch(Exception e)
            {
                return BadRequest($"Failed to create booking: {e.Message}");
            }
        }



        [HttpGet("gettallbookings")] 
        public async Task<ActionResult> GetAllBookings()
        {
            try
            {
                var allBookings = await _bookingServices.GettAllBookings();
                return Ok(allBookings);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"failed to get all bookings {e.Message}" });
            }
        }
    }
}
