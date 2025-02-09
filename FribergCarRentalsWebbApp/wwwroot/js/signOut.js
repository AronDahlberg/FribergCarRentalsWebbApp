$(document).ready(function () {
    $("#signOutButton").click(function () {

        $.ajax({
            url: "/Account/SignOut",
            type: "POST",
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    console.error("Sign out error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Sign out error", xhr, status, error);
                alert("An error occurred while signing out, please try again.");
            }
        });
    });
});