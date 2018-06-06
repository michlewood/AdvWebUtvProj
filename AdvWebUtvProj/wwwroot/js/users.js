function logIn() {
    let email = $("#logInEmail").val();

    $.ajax({
        url: 'api/users/signin',
        method: 'POST',
        data: { email: email }
    }).done(function (result) {
        alert("logged in");
        ShowStatus(result, 0);

        generateContent();
        }).fail(function (xhr, status, error) {
            alert(xhr.responseText);
        ShowStatus(xhr.responseText, 1);

    });
}

$("#logIn").click(function () {
    logIn();

});