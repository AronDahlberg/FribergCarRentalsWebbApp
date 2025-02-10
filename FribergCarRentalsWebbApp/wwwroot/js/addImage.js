$(document).ready(function () {
    $("#addImageForm").submit(function (e) {
        e.preventDefault();

        var formData = {
            imageLink: $("#addImageInput").val(),
            carId: $("#addImageCarId").val()
        };

        $.ajax({
            url: "/Booking/AddCarImage",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#addImageModal").modal("hide");
                    location.reload();
                } else {
                    console.error("Image adding error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Image adding error", xhr, status, error);
                alert("An error occurred while adding the image. Please try again.");
            }
        });
    });
});