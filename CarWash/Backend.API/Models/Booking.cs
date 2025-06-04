namespace Backend.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }

        public string Email { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ConfirmationToken { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
