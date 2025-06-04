namespace Backend.API.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; }

    }
}
