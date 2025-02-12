$(document).ready(function () {
    $("#unlistForm").submit(function (e) {
        e.preventDefault();

        var formData = {
            carId: $("#unlistCarId").val()
        };

        $.ajax({
            url: "/Booking/UnlistCar",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#unlistModal").modal("hide");

                    window.location.href = response.redirectUrl;
                } else {
                    console.error("Unlisting error", response);
                    alert("An error occurred while unlisting the car, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Unlisting error", xhr, status, error);
                alert("An error occurred while unlisting the car, please try again.");
            }
        });
    });
});