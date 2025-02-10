$(document).ready(function () {
    $(document).on('submit', '.deleteCustomerForm', function (e) {
        e.preventDefault();

        var $form = $(this);
        var customerId = $form.find("input[name='deleteCustomerAccountId']").val();

        $.ajax({
            url: "/Account/DeleteCustomer",
            type: "POST",
            data: { customerId: customerId },
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    $("#deleteCustomerModal-" + customerId).modal("hide");

                    location.reload();
                } else {
                    console.error("Deletion error", response);
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Deletion error", xhr, status, error);
                alert("An error occurred while deleting the customer, please try again.");
            }
        });
    });
});