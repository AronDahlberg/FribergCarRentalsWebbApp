$(document).ready(function () {
    $(document).on('submit', '.editCustomerForm', function (e) {
        e.preventDefault();

        var $form = $(this);
        var CurrentCustomerId = $form.find("input[name='editCustomerAccountId']").val();

        var formData = {
            customerId: CurrentCustomerId,
            email: $form.find("input[name='editCustomerEmail']").val(),
            password: $form.find("input[name='editCustomerPassword']").val()
        };
        
        $.ajax({
            url: "/Account/EditCustomer",
            type: "POST",
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#editCustomerModal-" + CurrentCustomerId).modal("hide");

                    location.reload();
                } else {
                    console.error("Editing error", response);
                    alert("An error occurred while changing the customer credentials, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Editing error", xhr, status, error);
                alert("An error occurred while changing the customer credentials, please try again.");
            }
        });
    });
});