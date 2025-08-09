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
     

        public BookingController(ServiceTypesService services)
        {
            _services = services;
             
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

    }
}
