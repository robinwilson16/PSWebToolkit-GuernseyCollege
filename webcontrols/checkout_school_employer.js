$(document).ready(function () {

    $(".slidingDiv1").show();
    $(".slidingDiv2").show();
    $(".show_hide1").show();
    $(".show_hide2").show();

    $('.show_hide1').click(function () {
        $(".slidingDiv1").slideToggle();
    });

    $('.show_hide2').click(function () {
        $(".slidingDiv2").slideToggle();
    });


    $('#cboEmployerID').change(function () {
        var value = $("#cboEmployerID option:selected").val();

        if (value > 0) {
            $('#txtEmployerName').prop('disabled', 'true');
        };
    });

    $(".select").keydown(function (event) {
        if (event.which != 46) // not delete key
            return;
        var sel = $(this);
        var val = sel.val();
        if (val != "")
            sel.find("option[value=" + val + "]").remove();
    });



});