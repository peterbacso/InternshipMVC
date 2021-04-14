"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("AddMember", function (user, id) {
    // Remember string interpolation
    $("#list").append(`<li class="member" member-id="${id}">
        <span class="name">${user}</span><span class="delete fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
    </li>`);
});

connection.on("DeleteMember", function (id) {
    var $li = $(`li.member[member-id=${id}]`);
    $li.remove();
});

connection.start().then(function () {
    console.log("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});