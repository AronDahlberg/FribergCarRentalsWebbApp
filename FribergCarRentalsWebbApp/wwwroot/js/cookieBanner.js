$(document).ready(function () {
    $('#accept-cookies').click(function () {
        // Set a session cookie so it doesn't show again in the current session
        document.cookie = "cookiesAccepted=true; path=/; SameSite=Strict;";

        $('#cookie-banner').fadeOut();
    });
});