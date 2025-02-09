$(document).ready(function () {
    $("#deleteAccountButton").click(function () {

        $.ajax({
            url: "/Account/DeleteAccount",
            type: "POST",
            success: function (response) {
                if (response.success) {
                    $("#deleteAccountModal").modal("hide");

                    window.location.href = response.redirectUrl;
                } else {
                    console.error("Email change error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Account deletion error", xhr, status, error);
                alert("An error occurred while deleting your account, please try again.");
            }
        });
    });
});