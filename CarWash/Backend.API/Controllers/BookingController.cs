using Backend.API.Models;
using Backend.API.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.ModelDTO;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/Booking")]
    public class BookingController : Controller
    {
        private readonly ServiceTypesService _services;
        private readonly BookingServices _bookingServices;
     

        public BookingController(ServiceTypesService services, BookingServices bookingServices)
        {
            _services = services;
            _bookingServices = bookingServices;
             
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
                var booking = await _bookingServices.CreateBookingAsync(request.LicensePlate, request.email,
                    request.timeSlotId, request.serviceTypeId);

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
    }
}
