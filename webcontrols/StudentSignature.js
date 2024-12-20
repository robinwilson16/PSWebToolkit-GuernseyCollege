$(document).ready(function () {
    $("#signature").jSignature()

    $("#signature").bind('change', function (e) {
        var datapair = $("#signature").jSignature("getData", "base30")
        $("#hidSignature").val(datapair);
    })

    $("#clear").bind('click', function (e) {
        var $sigdiv = $("#signature");
        $sigdiv.jSignature("reset");
    })

});