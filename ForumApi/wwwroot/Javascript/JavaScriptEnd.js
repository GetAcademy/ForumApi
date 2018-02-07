window.onload = function () {
    makeButton();
    getCategoryList();
};

function makeButton() {
    var cont = document.getElementById("makeCategoriesButton");
    var html = [];
    html.push("<button class='button' onclick='makeCategories()'>Make categories</button>");
    cont.innerHTML += html.join('');
}

function makeCategoryMenu(categories) {
    categories.forEach(function (element) {
        var categoryName = element.categoryName;
        var cont = document.getElementById("leftColumn");
        var html = [];
        html.push("<a class='category' onclick='setActive(this)' href='#" + categoryName + " /'>" + categoryName + "</a>");
        cont.innerHTML += html.join('');
    });
}

function makeCategories() {
    var category = {
        CategoryId: 0,
        CategoryName: ""
    };

    var html = document.getElementById('modalContent');
    html.innerHTML = '<h2>Make a new category</h2>';

        category.CategoryId++;
        category.CategoryName = ;
        var options = {};
        options.url = "/api/categories";
        options.type = "POST";
        $("#CategoryName").val(category.CategoryName);
        options.data = JSON.stringify(category);
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (msg) {
            $("#msg").html(msg);
            $("#CategoryName").val("");

        },
            options.error = function () {
                $("#msg").html("Error while calling the Web API!");
            };
        $.ajax(options);
    });
}

function getCategoryList() {
    $.ajax({
        url: '/api/categories',
        type: 'GET',
        dataType: 'json',
        success: function (categories) {
            makeCategoryMenu(categories);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function setActive(item) {
    $('.category').removeClass('active');
    $(item).addClass('active');
}

function goHome() {
    window.location = "../index.html";
}


