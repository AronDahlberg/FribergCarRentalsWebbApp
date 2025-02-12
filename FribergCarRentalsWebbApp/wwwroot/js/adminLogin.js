$(document).ready(function () {
    $("#admin-login-form").submit(function (e) {
        e.preventDefault();

        var formData = {
            email: $("#adminLoginEmail").val(),
            password: $("#adminLoginPassword").val()
        };

        $.ajax({
            type: "POST",
            url: "/Admin/Login",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    location.reload();
                } else {
                    console.error("Login error", response);
                    alert("An unexpected error occurred while loggin you in, please try again.");
                }
            },
            error: function (xhr, status, error) {
                if (xhr.status === 401) {
                    alert("Invalid email or password")
                } else {
                    console.error("Login error", xhr, status, error);
                    alert("An unexpected error occurred while loggin you in, please try again.");
                }
            }
        });
    });
});