$(document).ready(function () {
    //disability stuff
    toggleLearningDiffArea();

    $('#rdoLearnDiff input').change(function () {
        $('#cboLearningDiffOrDisID').val($(this).val());
        toggleLearningDiffArea();
    });
});
//CCCPS-80557: Moved logic of toggling visibility of Learning Diff Area to this new function as we need same on load as well.
function toggleLearningDiffArea() {
    if ($('#cboLearningDiffOrDisID').val() == "1") {
        $('.disfields').show();
    }
    else {
        $('.disfields').hide();
    }
}
