$("#btnChooseFile").click(function () {
    $('#fuAttachment').click();
});
function confirmDeletion() {
    var result = confirm("Are you sure you want to delete this attachment? Press OK to continue deletion.");
    if (result) {
        return true;
    }
    return false;
}