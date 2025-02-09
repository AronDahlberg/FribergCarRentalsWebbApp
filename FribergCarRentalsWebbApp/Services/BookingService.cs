using FribergCarRentalsWebbApp.Common;
using FribergCarRentalsWebbApp.Data;
using FribergCarRentalsWebbApp.Models;

namespace FribergCarRentalsWebbApp.Services
{
    public class BookingService(IBooking bookingRepository) : IBookingService
    {
        private readonly IBooking _bookingRepository = bookingRepository;

        public Booking? EagerGetById(int id)
        {
            return _bookingRepository.EagerGet(id);
        }

        /// <summary>
        /// Retrieves the date ranges during which the specified car is already booked.
        /// Only active (non-invalidated) bookings are considered.
        /// </summary>
        /// <param name="carId">The ID of the car for which to get unavailable date ranges.</param>
        /// <returns>A list of tuples, where Item1 is the pickup date and Item2 is the dropoff date.</returns>
        public List<(DateTime, DateTime)> GetUnavailableDateRanges(int carId)
        {
            // Retrieve all bookings for the specified car that have not been invalidated.
            var bookings = _bookingRepository.All().Where(b => b.Car.Id == carId && !b.Invalidated);

            var unavailableRanges = new List<(DateTime, DateTime)>();

            foreach (var booking in bookings)
            {
                // Because pickup and dropoff times are fixed (10:00 and 15:00 respectively),
                // only the Date part is used to represent the range.
                DateTime pickupDate = booking.PickupDateTime.Date;
                DateTime dropoffDate = booking.DropoffDateTime.Date;
                unavailableRanges.Add((pickupDate, dropoffDate));
            }

            return unavailableRanges;
        }

        public void CreateBooking(Booking booking)
        {
            _bookingRepository.Add(booking);
            _bookingRepository.Save();
        }

        public void CancelBooking(Booking booking)
        {
            ArgumentNullException.ThrowIfNull(booking, nameof(booking));

            if (booking.PickupDateTime < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Cannot cancel past or current bookings");
            }

            booking.Invalidated = true;
            booking.InvalidationDateTime = DateTime.UtcNow;

            string? bookingPayment = booking.Payments?.FirstOrDefault(p => p.TypeOfPayment == PaymentType.Booking)?.PaymentAmount;

            if (bookingPayment != null)
            {
                Payment refundPayment = new()
                {

                    PaymentAmount = bookingPayment,
                    TypeOfPayment = PaymentType.Refund,
                    ConfirmationDate = DateTime.UtcNow
                };

                booking.Payments!.Add(refundPayment);
            }

            _bookingRepository.Update(booking);
            _bookingRepository.Save();
        }
    }
}
