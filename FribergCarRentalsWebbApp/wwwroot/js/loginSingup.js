$(document).ready(function () {
    // Handle login form submission
    $("#login-form").submit(function (e) {
        e.preventDefault();

        var formData = {
            email: $("#loginEmail").val(),
            password: $("#loginPassword").val()
        };

        $.ajax({
            type: "POST",
            url: "/CustomerAccount/Login",
            data: formData,
            dataType: "json", // Expect JSON response
            success: function (response) {
                $("#loginSignupModal").modal("hide");
                location.reload();
            },
            error: function (xhr, status, error) {
                if (xhr.status === 401) {
                    alert("Invalid email or password")
                } else {
                    console.error("Login error", xhr, status, error);
                    alert("An unexpected error occurred. Please try again later.");
                }
            }
        });
    });

    // Handle signup form submission
    $("#signup-form").submit(function (e) {
        e.preventDefault();

        if ($("#signupPassword").val() === $("#signupConfirmPassword").val()) {
            var formData = {
                email: $("#signupEmail").val(),
                password: $("#signupPassword").val()
            };

            $.ajax({
                type: "POST",
                url: "/CustomerAccount/Signup",
                data: formData,
                dataType: "json", // Expect JSON response
                success: function (response) {
                    $("#loginSignupModal").modal("hide");
                    location.reload();
                },
                error: function (xhr, status, error) {
                    if (xhr.status === 409) {
                        alert("The email is already in use")
                    } else {
                        console.error("Singup error", xhr, status, error);
                        alert("An unexpected error occurred. Please try again later.");
                    }
                }
            });
        } else {
            alert("Passwords do not match")
        }
    });
});