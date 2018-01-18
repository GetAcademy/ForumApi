$(document).ready(function () {
    getPostList();
});

var Post = {
    postId: 0,
    postTitle: "",
    postContent: "",
    createdBy: "",
    createdOn: "",
    updatedBy: "",
    updatedOn: "",
    Answers: ""
}

var Answer = {
    answerId: 0,
    answerContent: "",
    createdBy: "",
    createdOn: "",
    updatedBy: "",
    updatedOn: ""
}

function getPostList() {
    $.ajax({
        url: '/api/GetPosts/',
        type: 'GET',
        dataType: 'json',
        success: function (posts) {
            postListSuccess(posts);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function postListSuccess(posts) {
    $("#postTable tbody").remove();
    $.each(posts, function (index, post) {
        postAddRow(post);
    });
}

function postAddRow(post) {
    if ($("#postTable tbody").length == 0) {
        $("#postTable").append("<tbody></tbody>");
    }
    $("#postTable tbody").append(
        postBuildTableRow(post));
}

function postBuildTableRow(post) {
    var newRow = "<tr>" +
        "<td>" + post.postId + "</td>" +
        "<td><input   class='input-postTitle' type='text' value='" + post.postTitle + "'/></td>" +
        "<td><input   class='input-postContent' type='text' value='" + post.postContent + "'/></td>" +
        "<td><label   >" + post.createdBy + "</label></td>" +
        "<td><label   >" + post.createdOn + "</label></td>" +
        "<td><label   >" + post.updatedBy + "</label></td>" +
        "<td><label   >" + post.updatedOn + "</label></td>" +
        "<td><label   >" + post.Answers + "</label></td>" +
        "<td>" +
        "<button type='button' " +
        "onclick='postUpdate(this);' " +
        "class='btn btn-default' " +
        "data-id='" + post.postId + "' " +
        ">" +
        "Update" +
        "</button> " +
        " <button type='button' " +
        "onclick='postDelete(this);'" +
        "class='btn btn-default' " +
        "data-id='" + post.postId + "'>" +
        "Delete" +
        "</button>" +
        " <button type='button' " +
        "onclick='createAnswer(this);'" +
        "class='btn btn-default' " +
        "data-id='" + post.postId + "'>" +
        "Answer" +
        "</button>" +
        "</td>" +
        "</tr>";

    return newRow;
}

function onAddPost(item) {
    Post.postId++;
    var options = {};
    options.url = "/api/AddPost";
    options.type = "POST";
    var obj = Post;
    obj.postTitle = $("#postTitle").val();
    obj.postContent = $("#postContent").val();
    console.log('in onAddPost');
    console.dir(obj);
    options.data = JSON.stringify(obj);
    options.contentType = "application/json";
    options.dataType = "html";

    options.success = function (msg) {
        getPostList();
        $("#msg").html(msg);
        $("#postTitle").val("");
        $("#postContent").val("");
    },
        options.error = function () {
            $("#msg").html("Error while calling the Web API!");
        };
    $.ajax(options);
}

function postUpdate(item) {
    var id = $(item).data("id");
    var options = {};
    options.url = "/api/UpdatePost/";
    options.type = "PUT";

    var obj = Post;
    obj.postId = id;
    obj.postTitle = $(".input-postTitle", $(item).parent().parent()).val();
    obj.postContent = $(".input-postContent", $(item).parent().parent()).val();
    console.log("postUpdate id: " + id);
    console.dir(obj);
    options.data = JSON.stringify(obj);
    options.contentType = "application/json";
    options.dataType = "html";
    options.success = function (msg) {
        getPostList();
        $("#msg").html(msg);
    };
    options.error = function () {
        $("#msg").html("Error while calling the Web API!");
    };
    $.ajax(options);
}

function postDelete(item) {
    var id = $(item).data("id");
    var options = {};
    options.url = "/api/DeletePost/" + id;
    options.type = "DELETE";
    options.dataType = "html";
    options.success = function (msg) {
        console.log('msg= ' + msg);
        $("#msg").html(msg);
        getPostList();
    };
    options.error = function () {
        $("#msg").html("Error while calling the Web API!");
    };
    $.ajax(options);
}

function handleException(request, message, error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" + request.responseJSON.Message + "\n";
    }

    alert(msg);
}