namespace Backend.API.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }

    }
}
sasa