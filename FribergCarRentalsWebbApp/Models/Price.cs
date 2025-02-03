namespace FribergCarRentalsWebbApp.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int AllowedDailyMileage { get; set; }
        public int AdditionalMileagePriceInterval { get; set; }
        public int DailyPrice { get; set; }
        public int AdditionalMileagePricePerInterval { get; set; }
        public DateTime PriceCreationDate { get; set; }
    }
}
