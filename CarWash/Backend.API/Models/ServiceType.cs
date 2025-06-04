namespace Backend.API.Models
{
    public class ServiceType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public Decimal Price { get; set; }
 
    }
}
