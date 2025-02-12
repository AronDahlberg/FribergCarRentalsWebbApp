$(document).ready(function () {
    $(document).on('submit', '.cancelCustomerBookingForm', function (e) {
        e.preventDefault();

        var $form = $(this);
        var bookingId = $form.find("input[name='cancelCustomerBookingId']").val();

        $.ajax({
            url: "/Booking/CancelBooking",
            type: "POST",
            data: { id: bookingId },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#cancelCustomerBookingModal-" + bookingId).modal("hide");

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