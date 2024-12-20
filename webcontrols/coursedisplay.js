$(document).ready(function () {
    // Hide & show details on fees (without popping the browser window to the top on each click!)
    $(".slidingDiv").hide();
    $(".show_hide").show();

    $('.show_hide').click(function (e) {
        $(".slidingDiv").slideToggle();
        e.preventDefault();
    });
    // End hide & show
});