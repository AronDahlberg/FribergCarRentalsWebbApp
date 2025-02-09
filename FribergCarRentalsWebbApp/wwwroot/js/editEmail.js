$(document).ready(function () {
    $("#editEmailModal .btn-primary").click(function () {
        var newEmail = $("#emailInput").val();

        $.ajax({
            url: "/Account/ChangeEmail",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ email: newEmail }),
            success: function (response) {
                if (response.success) {
                    $("#editEmailModal").modal("hide");
                    location.reload();
                } else {
                    console.error("Email change error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Email change error", xhr, status, error);
                alert("An error occurred while updating the email.");
            }
        });
    });
});