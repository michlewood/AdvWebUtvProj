function handleError(xhr, status, error, id) {
    if (xhr.status === 404) {
        if (id) {
            alert(`Thing with id=${id} not found`);
        } else {
            alert(`Not found`);
        }

    } else if (xhr.status === 400) {
        var errList = [];
        var response = xhr.responseJSON;

        if (response.TypeOfThing) {
            errList.push(`*  ${response["TypeOfThing"]}`);
        }

        if (response.NameOfThing) {
            errList.push(`*  ${response["NameOfThing"]}`);
        }

        if (response.PriceOfThing) {
            errList.push(`*  ${response["PriceOfThing"]}`);
        }

        var errorString = errList.join("\n");

        alert(`Bad request\n${errorString}`);
        console.log("response", response);

    } else {
        alert(`Unexpected error`);
    }

    console.log(xhr, status, error, id);
}

$("#addThingButton").click(function () {
    var form = $("#addForm");

    var typeOfThing = $("[name=typeOfThing]", form).val();
    var nameOfThing = $("[name=nameOfThing]", form).val();
    var priceOfThing = $("[name=priceOfThing]", form).val();

    $.ajax({
        url: '/api/things/',
        method: 'Post',
        data: {
            "TypeOfThing": typeOfThing,
            "NameOfThing": nameOfThing,
            "PriceOfThing": priceOfThing
        }
    })
        .done(function (id) {
            alert(`Thing with id=${id} added`);
        })
        .fail(function (xhr, status, error) {
            handleError(xhr, status, error);
        });
});

$("#updateThingButton").click(function () {
    var form = $("#updateForm");
    
    var id = $("[name=id]", form).val();
    var typeOfThing = $("[name=typeOfThing]", form).val();
    var nameOfThing = $("[name=nameOfThing]", form).val();
    var priceOfThing = $("[name=priceOfThing]", form).val();

    $.ajax({
        url: '/api/things/',
        method: 'PUT',
        data: {
            "Id": id,
            "TypeOfThing": typeOfThing,
            "NameOfThing": nameOfThing,
            "PriceOfThing": priceOfThing
        }
    })
        .done(function () {
            alert(`Thing with id=${id} updated`);
        })
        .fail(function (xhr, status, error) {
            handleError(xhr, status, error, id);
        });
});

$("#removeThingButton").click(function () {

    var id = $("#removeForm [name=id]").val();

    $.ajax({
        url: '/api/things/' + id,
        method: 'DELETE'
    })
        .done(function () {
            alert(`Thing with id=${id} deleted`);
        })
        .fail(function (xhr, status, error) {
            handleError(xhr, status, error, id);
        });
});

$("#seedButton").click(function () {

    $.ajax({
        url: '/api/things/seed',
        method: 'POST'
    })
        .done(function () {
            alert(`Things seeded`);
        })
        .fail(function (xhr, status, error) {
            handleError(xhr, status, error);
        });
});

$("#countButton").click(function () {

    $.ajax({
        url: '/api/things/count',
        method: 'GET'
    })
        .done(function (result) {
            alert(`Number of things: ${result}`);
        })
        .fail(function (xhr, status, error) {
            handleError(xhr, status, error);
        });
});