namespace DotNetCourseNew.Models
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }
        
        public int AdressId { get; set; }

        public AddressDTO Address { get; set; }

        public List<DishDTO> Dishes { get; set; }
    }
}