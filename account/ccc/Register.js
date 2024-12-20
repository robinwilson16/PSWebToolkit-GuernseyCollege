function check(input) {
    if (input.value !== $("#txtPassword").val()) {
        input.setCustomValidity('Password does not match.');
    } else {
        // input is fine -- reset the error message
        input.setCustomValidity('');
    }
}