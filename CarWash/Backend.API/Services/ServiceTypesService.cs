using Backend.API.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Models.ModelDTO;

namespace Backend.API.Services
{
    public class ServiceTypesService
    {
        private readonly AppDbContext _context;

        public ServiceTypesService(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// return all the service types as list
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeDTO>> GetAllServiceTypes()
        {
            return await _context.ServiceTypes.Select(st => new ServiceTypeDTO
            {
                Id = st.Id,
                Description = st.Description,
                Name = st.Name,
                Price = st.Price

            }).ToListAsync();
        }

        public async Task<List<TimeSlotDTO>> GetAllTimeSlots()
        {
            return await _context.TimeSlots.Select(ts => new TimeSlotDTO
            {
                Id = ts.Id,
                AppointmentDate = ts.AppointmentDate,
                EndTime = ts.EndTime,
                IsAvailable = ts.IsAvailable,
                StartTime = ts.StartTime,

            }).ToListAsync();
        }
       
         

    }
}
