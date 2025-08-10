using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.API.Requests
{
    public class BookingRequest
    {
        [Required]
        [JsonPropertyName("licensePlate")]
        public string licensePlate { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string email { get; set; }

        [Required]
        [JsonPropertyName("serviceTypeId")]
        public int serviceTypeId { get; set; }

        [Required]
        [JsonPropertyName("timeSlotId")]
        public int timeSlotId { get; set; }
    }
}
