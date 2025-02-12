$(document).ready(function () {
    // When the user presses Enter in any image link input
    $('#createListingImageLinks').on('keydown', '.create-listing-image-link-input', function (e) {
        if (e.key === "Enter") {
            e.preventDefault();

            var $input = $(this);
            var url = $.trim($input.val());
            var $li = $input.closest('li');
            var isNewItem = $li.is(':last-child');

            if (url === "") {
                // For an edit field (not the new blank input), revert back to its previous value
                if (!isNewItem) {
                    var oldVal = $input.data('old') || "";
                    $li.empty().append(
                        $('<span class="create-listing-image-link-text text-nowrap overflow-auto" style="cursor: pointer; max-width: 700px;"></span>').text(oldVal)
                    ).append(
                        $('<button type="button" class="btn btn-danger btn-sm float-end create-listing-delete-link">X</button>')
                    );
                }
                return false;
            }

            $li.empty().append(
                $('<span class="create-listing-image-link-text text-nowrap overflow-auto" style="cursor: pointer; max-width: 700px;"></span>').text(url)
            ).append(
                $('<button type="button" class="btn btn-danger btn-sm float-end create-listing-delete-link">X</button>')
            );

            // If this was the last input, append a new blank input field for additional links
            if (isNewItem) {
                $('#createListingImageLinks').append(
                    $('<li class="list-group-item"></li>').append(
                        $('<input type="text" class="form-control create-listing-image-link-input" placeholder="Enter image URL and press Enter">')
                    )
                );
            }
        }
    });

    // When the user clicks on a link text, turn it into an input field
    $('#createListingImageLinks').on('click', '.create-listing-image-link-text', function () {
        var $li = $(this).closest('li');
        $li.find('.create-listing-delete-link').remove();
        var currentVal = $(this).text();
        // Store current value (for revert if empty)
        var $input = $('<input type="text" class="form-control create-listing-image-link-input">')
            .val(currentVal)
            .data('old', currentVal);
        $(this).replaceWith($input);
        $input.focus();
    });

    // Delete an image link when the user clicks the delete button
    $('#createListingImageLinks').on('click', '.create-listing-delete-link', function () {
        $(this).closest('li').remove();
    });

    // When form is submitted
    $('#createListingForm').submit(function (e) {
        e.preventDefault();

        var formData = {
            carName: $('#createListingCarName').val(),
            description: $('#createListingDescription').val(),
            dailyPrice: $('#createListingDailyPrice').val(),
            allowedMileage: $('#createListingAllowedMileage').val(),
            pricePerExtraMile: $('#createListingPricePerExtraMile').val(),
            imageLinks: []
        };

        // Loop through the list to collect image URLs.
        $('#createListingImageLinks li').each(function () {
            var $li = $(this);
            var linkText = $li.find('.create-listing-image-link-text').text();
            if (linkText) {
                formData.imageLinks.push(linkText);
            }
        });

        $.ajax({
            url: '/Booking/CreateListing',
            type: 'POST',
            data: formData,
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    alert("Succesfully created listing.");

                    window.location.href = response.redirectUrl;
                } else {
                    console.error("Listing creation error", response);
                    alert("An error occurred while creating the listing, please try again.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Listing creation error", xhr, status, error);
                alert("An error occurred while creating the listing, please try again.");
            }
        });
    });
});