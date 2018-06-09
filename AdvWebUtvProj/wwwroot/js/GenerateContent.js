function GenerateThings(role) {
    let updateContent;
    updateContent += GenerateSeed();
    updateContent += GenerateCount();
    updateContent += GenerateShowAll();
    if (role !== "Anonymous") updateContent += GenerateAdd();
    if (role !== "Anonymous") updateContent += GenerateRemove();
    if (role !== "Anonymous") updateContent += GenerateUpdate();

    $("#update").html(updateContent);

}

function GenerateSeed() {
    let generateSeed =
        '<tr>'
        + '    <td><h3>Seed:</h3></td>'
        + '    <td><button id="seedButton">Seed</button></td>'
        + '</tr>';
    return generateSeed;
}

function GenerateCount() {
    let generateCount =
        '    <tr>'
        + '        <td><h3>Räkna:</h3></td>'
        + '        <td> <button id="countButton">Räkna</button></td>'
        + '    </tr>';
    return generateCount;
}

function GenerateShowAll() {
    let generateShowAll =
        '<tr>'
        + '    <td><h3>Visa alla:</h3></td>'
        + '    <td>'
        + '        <p>Gå till en ny sida som visar alla nyheter i databasen (som json)</p>'
        + '        <a href="/api/things">Visa alla nyheter</a>'
        + '    </td>'
        + '</tr>';
    return generateShowAll;
}

function GenerateAdd() {
    let generateAdd =
        '<tr>'
        + '    <td><h3>Lägg till:</h3></td>'
        + '    <td>'
        + '        <div id="addForm">'
        + '            Type Of Thing: <input name="typeOfThing" type="text" value="" />'
        + '            Name Of Thing: <input name="nameOfThing" type="text" value="" />'
        + '            Price Of Thing: <input name="priceOfThing" type="number" value="" />'
        + '            <button id="addThingButton">Lägg till</button>'
        + '        </div>'
        + '    </td>'
        + '    </tr>';
    return generateAdd;
}

function GenerateRemove() {
    let generateRemove =
        '   <tr>'
        + '       <td><h3>Ta bort:</h3></td>'
        + '       <td>'
        + '           <div id="removeForm">'
        + '               Id:<input name="id" type="text" value="" />'
        + '               <button id="removeThingButton">Ta bort</button>'
        + '           </div>'
        + '       </td>'
        + '   </tr>';
    return generateRemove;
}

function GenerateUpdate() {
    let updateContent =
        '<td><h3>Uppdatera:</h3></td>'
        + '<td>'
        + '    <div id="updateForm">'
        + '        <table>'
        + '            <tr>'
        + '                <td> Id of thing to change: </td>'
        + '                <td><input name="id" type="number" value="" /></td>'
        + '            </tr>'
        + '            <tr>'
        + '                <td>Type Of Thing:'
        + '                    <td><input name="typeOfThing" type="text" value="" />'
        + '                        <td>Name Of Thing:'
        + '                    <td><input name="nameOfThing" type="text" value="" />'
        + '                                <td>Price Of Thing:'
        + '                    <td><input name="priceOfThing" type="number" value="" />'
        + '                </tr>'
        + '                                    <tr>'
        + '                                        <td><button id="updateThingButton">Update Thing</button>'
        + '                </tr>'
        + '            </table>'
        + '        </div>'
        + '                            </td>';
    return updateContent;
}

function GenerateLogin(role) {
    let updateLogin;
    if (role === "Anonymous") {
        updateLogin =
            '<input type="text" id="logInEmail" />'
            + '<button class="btn btn-primary btn-sm" id="logIn">Log in</button>'
            + '<br />'
            + '<input id = "userEmail" type = "text" />'
            + '<button class="btn btn-success btn-sm" id="createUser">Register</button>';
    }
    else {
        updateLogin = '<button class="btn btn-danger btn-sm" id="logOut">Log out</button>';
    }
    $("#loginDiv").html(updateLogin);

}

function GenerateUserStuff(role) {
    let userstuff;
    if (role[0] === "Admin") {
        userstuff =
            '<hr />'
            + '   <h3>Users</h3>'
            + '   <button id="getUsers">Show All Users</button>'
            + '   <div id="userTable"></div>';
    }
    else {
        userstuff = "";
    }
    $("#Users").html(userstuff);
}

function GenerateUsersTable(result) {
    let message =
        '<table>' +
        '<thead>' +
        '<tr>' +
        '<th>#</th>' +
        '<th >Name</th>' +
        '<th >Email</th>' +
        '<th> Role </th>' +
        '<th> Change role </th>' +
        '</tr>' +
        '</thead>' +
        '<tbody>';
    let numberInList = 1;
    $.each(result, function (index, item) {
        message += '<tr>';
        message += '<th>' + numberInList + '</th>';
        message += '<td>' + item.name + '</td>';
        message += '<td>' + item.email + '</td>';
        message += '<td>' + item.role + '</td>';
        message += '<td> <button id="' + item.email + '" class="changeRole">change role</button ></td > ';
        message += '</tr>';
        numberInList++;

    });
    message += '</tbody ></table >';

    $("#userTable").html(message);
    $(".changeRole").click(function () {
        changeRole(this.id);
    });
}

function EmptyShowAllUser() {
    $("#userTable").html("");
}