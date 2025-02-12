$(document).ready(function () {
    $("#editEmailForm").submit(function (e) {
        e.preventDefault();

        var formData = {
            email: $("#emailInput").val()
        };

        $.ajax({
            url: "/Account/ChangeEmail",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#editEmailModal").modal("hide");
                    location.reload();
                } else {
                    console.error("Email change error", response);
                    alert("An error occurred while updating the email, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Email change error", xhr, status, error);
                alert("An error occurred while updating the email, please try again.");
            }
        });
    });
});