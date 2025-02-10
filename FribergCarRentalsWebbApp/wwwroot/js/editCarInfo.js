$(document).ready(function () {
    $("#editCarInfoForm").submit(function (e) {
        e.preventDefault();

        var formData = {
            carName: $('#editCarInfoCarName').val(),
            description: $('#editCarInfoDescription').val(),
            dailyPrice: $('#editCarInfoDailyPrice').val(),
            allowedMileage: $('#editCarInfoAllowedMileage').val(),
            pricePerExtraMile: $('#editCarInfoPricePerExtraMile').val(),
            carId: $('#editCatInfoCarId').val()
        };

        $.ajax({
            url: "/Booking/EditCarInfo",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#editCarInfoModal").modal("hide");
                    location.reload();
                } else {
                    console.error("Car info change error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Car info change error", xhr, status, error);
                alert("An error occurred while updating the new car information. Please try again.");
            }
        });
    });
});