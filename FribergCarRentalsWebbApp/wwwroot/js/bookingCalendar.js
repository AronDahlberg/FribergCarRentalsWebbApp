$(document).ready(function () {
    // Initialize the daterangepicker on the #dateRange input.
    $('#dateRange').daterangepicker({
        autoApply: false,
        locale: {
            format: 'MM/DD/YYYY'
        },
        minDate: moment(), // disable past dates
        isInvalidDate: function (date) {
            // Loop through each unavailable (booked) range.
            for (var i = 0; i < unavailableRanges.length; i++) {
                var start = moment(unavailableRanges[i].start, 'MM/DD/YYYY');
                var end = moment(unavailableRanges[i].end, 'MM/DD/YYYY');
                // Disable dates within any booked period (inclusive).
                if (date.isBetween(start, end, 'day', '[]')) {
                    return true;
                }
            }
            return false;
        }
    });

    // When the user applies a date range, update the hidden fields.
    $('#dateRange').on('apply.daterangepicker', function (ev, picker) {
        var start = picker.startDate;
        var end = picker.endDate;
        var invalidFound = false;

        // Iterate over each day in the selected range.
        for (var day = start.clone(); day.isSameOrBefore(end, 'day'); day.add(1, 'day')) {
            for (var i = 0; i < unavailableRanges.length; i++) {
                var bookedStart = moment(unavailableRanges[i].start, 'MM/DD/YYYY');
                var bookedEnd = moment(unavailableRanges[i].end, 'MM/DD/YYYY');
                if (day.isBetween(bookedStart, bookedEnd, 'day', '[]')) {
                    invalidFound = true;
                    break;
                }
            }
            if (invalidFound) break;
        }

        if (invalidFound) {
            alert("The selected date range includes one or more dates that are already booked. Please choose a valid range.");
            // Optionally, clear the selection by resetting the daterangepicker:
            $(this).data('daterangepicker').setStartDate(moment());
            $(this).data('daterangepicker').setEndDate(moment());
            // You may also want to clear any related hidden fields.
            $('#pickupDateTime, #dropoffDateTime, #totalPrice').val('');
            $('#totalPriceDisplay').text('$' + dailyPrice);
            return;
        }

        // Pickup time is always 10:00 on the start date.
        var pickupDate = start.clone().hour(10).minute(0).second(0);
        // Dropoff time is always 15:00 on the end date.
        var dropoffDate = end.clone().hour(15).minute(0).second(0);

        $('#pickupDateTime').val(pickupDate.format('YYYY-MM-DDTHH:mm:ss'));
        $('#dropoffDateTime').val(dropoffDate.format('YYYY-MM-DDTHH:mm:ss'));

        // Calculate the number of rental days (same-day selection counts as 1 day).
        var days = end.diff(start, 'days') + 1;
        var totalPrice = dailyPrice * days;
        $('#totalPrice').val(totalPrice);

        $('#totalPriceDisplay').text('$' + totalPrice);
        $('#totalMileageDisplay').text(dailyMileage * days);
    });
});