using System.ComponentModel.DataAnnotations;

namespace Backend.API.Requests
{
    public class BookingRequest
    {
        [Required]
        [StringLength(6)]
        public string licensePlate { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public int serviceTypeId { get; set; }

        [Required]
        public int timeSlotId { get; set; }
    }
}
