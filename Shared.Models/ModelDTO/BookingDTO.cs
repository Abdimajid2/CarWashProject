using Shared.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.ModelDTO
{
   public class BookingDTO
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }

        public string Email { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ServiceTypeDTO ServiceType { get; set; }
        public TimeSlotDTO TimeSlot { get; set; }
         
    }
}
