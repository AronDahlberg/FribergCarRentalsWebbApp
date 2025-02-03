namespace FribergCarRentalsWebbApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Account Account { get; set; }
        public List<Payment> Payments { get; set; }
        public DateTime BookingCreation { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime DropoffDateTime { get; set; }
        public bool Invalidated { get; set; }
        public DateTime? InvalidationDateTime { get; set; }
        public int? DeltaMileageAtReturn { get; set; }
        public bool? DamagedAtReturn { get; set; }
        public bool? UncleanedAtReturn { get; set; }
        public bool? UnfilledTankAtReturn { get; set; }
    }
}
