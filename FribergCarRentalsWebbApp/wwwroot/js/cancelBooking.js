$(document).ready(function () {
    $(".confirmBookingCancelation").click(function () {
        var bookingId = $(this).data("booking-id");

        $.ajax({
            url: "/Booking/CancelBooking",
            type: "POST",
            data: { id: bookingId },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    alert("Booking successfully canceled");
                    location.reload();
                } else {
                    console.error("Error canceling booking", response);
                    alert("An error occurred while canceling the booking, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error canceling booking", xhr, status, error);
                alert("An error occurred while canceling the booking, please try again.");
            }
        });
    });
});