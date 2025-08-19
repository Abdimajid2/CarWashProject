
using Shared.Models.ModelDTO;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Customer.UI.Services
{
    public class BookingService
    {
        private readonly HttpClient _httpClient;

        string baseurl;
        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            baseurl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:5184";
        }

        public async Task<List<TimeSlotDTO>> GetTimeslots()
        {
            var response = await _httpClient.GetAsync($"{baseurl}/api/Booking/timeslots");

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                //return empty list if deserialization fails
                return JsonSerializer.Deserialize<List<TimeSlotDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TimeSlotDTO>();
            }

            return new List<TimeSlotDTO>();
        }



        public async Task<List<ServiceTypeDTO>> GetServiceTypes()
        {
            var response = await _httpClient.GetAsync($"{baseurl}/api/Booking/servicetypes");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                //return empty list if deserialization fails
                return JsonSerializer.Deserialize<List<ServiceTypeDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ServiceTypeDTO>();
            }

            return new List<ServiceTypeDTO>();
        }

        public async Task<bool> CreateBooking(string licensePlate, string email, int serviceTypeId, int timeSlotId)
        {

            var bookingData = new {licensePlate, email, serviceTypeId, timeSlotId};

            var json = JsonSerializer.Serialize(bookingData);
            Debug.WriteLine($"JSON being sent: {json}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseurl}/api/Booking/createbooking", content);

            return response.IsSuccessStatusCode;
        }



    }
}
