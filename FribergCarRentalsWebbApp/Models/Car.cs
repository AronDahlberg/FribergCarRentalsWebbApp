namespace FribergCarRentalsWebbApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public List<Price> Prices { get; set; }
        public List<Booking> Bookings { get; set; }
        public DateTime ListingCreationDate { get; set; }
        public bool Unlisted { get; set; }
    }
}
