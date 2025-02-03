namespace FribergCarRentalsWebbApp.Models
{
    public class Customer : Account
    {
        public List<Booking>? Bookings { get; set; }
    }
}
