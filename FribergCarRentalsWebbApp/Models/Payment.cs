namespace FribergCarRentalsWebbApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentAmount { get; set; }
        public PaymentType TypeOfPayment { get; set; }
        public Booking Booking { get; set; }
        public DateTime ConfirmationDate { get; set; }
    }
}
