using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Request
{
    public class MakingBookingRequest
    {

        public string LicensePlate { get; set; }

        public string Email { get; set; }

        public int ServiceTypeId { get; set; }

        public int TimeSlotId { get; set; }
    }
}
