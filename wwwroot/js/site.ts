// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function validateInput(input: string, wordLength: Number) {
    if (input.length != wordLength) {
        return false
    }

    // Check if guess is
    var regex = new RegExp("^[A-Z]+$");
    if (!regex.test(input)) {
        return false;
    }

    // All test passed
    return true;
}