$(document).ready(function () {
    $("#signOutButton").click(function () {

        $.ajax({
            url: "/Account/LogOut",
            type: "POST",
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    console.error("Sign out error", response);
                    alert("An error occurred while signing out, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Sign out error", xhr, status, error);
                alert("An error occurred while signing out, please try again.");
            }
        });
    });
});