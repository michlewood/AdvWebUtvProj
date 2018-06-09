$(function () {
    FirstUpdateStuff();

    $("#logInAdmin").click(function () {
        login("admin@gmail.com");
    });

    $("#logInMember").click(function () {
        login("test@test.se");
    });

    $("#seed").click(function () {
        Seed();
    });
});

function FirstUpdateStuff() {
    $("#update").html();

    $.ajax({
        url: 'api/users/getrole',
        method: 'GET'
    }).done(function (resultRole) {
        updateUserInfo(resultRole);
        GenerateLogin(resultRole);
        GenerateThings(resultRole);
        GenerateUserStuff(resultRole);

        Buttons();
        ThingsButtons();

    }).fail(function (xhr, status, error) {
        alert(xhr.responseText);
        xhr.responseText;
    });
}

function UpdateStuff() {
    $("#update").html();
    //updateUserInfo();

    $.ajax({
        url: 'api/users/getrole',
        method: 'GET'
    }).done(function (resultRole) {
        updateUserInfo(resultRole);
        GenerateLogin(resultRole);
        GenerateThings(resultRole);
        GenerateUserStuff(resultRole);

        Buttons();
        ThingsButtons();
    }).fail(function (xhr, status, error) {
        alert(xhr.responseText);
        xhr.responseText;
    });
}

function login(email = $("#logInEmail").val()) {
    $.ajax({
        url: 'api/users/signin/' + email,
        method: 'GET'
        //data: { email: email }
    }).done(function (result) {
        UpdateStuff();
        //alert("logged in");

    }).fail(function (xhr, status, error) {
        console.log(xhr.responseText);
    });
}

function logout() {
    $.ajax({
        url: 'api/users/signout',
        method: 'POST'

    }).done(function (result) {
        UpdateStuff();
        EmptyShowAllUser();
    });
}

function createUser() {
    let email = $("#userEmail").val();

    $.ajax({
        url: 'api/users/adduser/' + email,
        method: 'POST'
    }).done(function (result) {
        console.log(result);
    }).fail(function (xhr, status, error) {
        console.log(xhr.responseText, 1);

    });
}

function updateUserInfo(RoleAndName) {

    let print = RoleAndName !== "Anonymous" ? RoleAndName[1] + ": " + RoleAndName[0] : RoleAndName

    $("#userInfo").text(print);
}

function getAllUsers() {
    $.ajax({
        url: 'api/users',
        method: 'GET'

    }).done(function (result) {
        GenerateUsersTable(result);


    });
}

function Buttons() {
    $("#changeSubmit").click(function () {
        changeSubmit();
    });


    $("#createUser").click(function () {
        createUser();
    });

    $("#logIn").click(function () {
        login();

    });



    $("#logOut").click(function () {
        logout();
    });

    $("#getUsers").click(function () {
        getAllUsers();
    });
}

function changeRole(email) {
    $.ajax({
        url: 'api/users/editRole/' + email,
        method: 'POST'
    }).done(function (result) {
        getAllUsers();

    }).fail(function (xhr, status, error) {
        console.log(xhr.responseText, 1);

    });
}

function Seed() {
    logout();

    $.ajax({
        url: 'api/users/seed',
        method: 'Get'

    }).done(function (result) {

    });
}

