using Backend.API.Data;
using Backend.API.Seeds;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("seeddb")]
    public class SeedDbController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeedDbController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
           
                SeedDb.seedDb(_context);

                return Ok(new { message = "db seeded" });
          
        }

    }
}
