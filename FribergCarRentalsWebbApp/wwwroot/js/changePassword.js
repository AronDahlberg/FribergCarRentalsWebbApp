$(document).ready(function () {
    $("#changePasswordForm").submit(function (e) {
        e.preventDefault();

        var newPassword = $("#newPassword").val();

        if (newPassword === $("#confirmPassword").val()) {

            var formData = {
                currentPassword: $("#currentPassword").val(),
                newPassword: newPassword
            };

            $.ajax({
                url: "/Account/ChangePassword",
                type: "POST",
                data: formData,
                dataType: "json",
                success: function (response) {
                    if (response.success) {
                        $("#changePasswordModal").modal("hide");
                        alert("Succesfully changed password.")
                    } else {
                        console.error("Password change error", response);
                        alert("Error: " + response.message);
                    }
                },
                error: function (xhr, status, error) {
                    if (xhr.status === 401) {
                        alert("Invalid password.");
                        $("#currentPassword").val('');
                    } else {
                        console.error("Password change error", xhr, status, error);
                        alert("An error occurred while updating the password.");
                    }
                }
            });
        } else {
            alert("Passwords do not match");
            $("#newPassword, #confirmPassword").val('');
        }
    });
});