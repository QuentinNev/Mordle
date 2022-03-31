// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/// TODO : Make a method to prevent invalid input in game page only

function validate(input, wordlLenght) {
    var response = checkInput(input, wordlLenght)

    if (response == "success") {
        return true;
    }
    else {
        alert(response);
        return false;
    }
}

function checkInput(input, wordLength) {
    if (input.length < wordLength) {
        return "Word is not long enough!"
    }

    // Check if guess is
    var regex = new RegExp("^[A-Z]+$")
    if (!regex.test(input)) {
        return "Word must contain only alphabetical characters"
    }

    // All test passed
    return "success";
}