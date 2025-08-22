using Shared.Models.Enum;

namespace Backend.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }

        public string Email { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ConfirmationToken { get; set; }

        public bool IsEmailConfirmed { get; set; }

        //nav prop
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceTypes ServiceType { get; set; }
    }
}
