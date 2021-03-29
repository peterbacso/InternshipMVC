// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`
                    <li class="member">
                        <span class="name">${data}</span>
                        <span class="delete fa fa-remove"></span>
                        <i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
                    </li>
                `);

                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });
    })
    $("#clear").click(function () {
        $("#newcomer").val("");
    })
    $("#list").on('click', '.delete', function () {
        var parent = $(this).closest('li');
        var index = parent.index();

        $.ajax({
            method: 'DELETE',
            url: `/home/RemoveMember?index=${index}`,
            success: function (data) {
                $(parent).remove();
            },
            error: function (data) {
                alert(`failed to remove ${index}`);
            },
        });

    })
    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        console.log('submit changes to server');
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })
    $("#editClassmate").on('click', '#submit', function () {
        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");

        $.ajax({
            method: 'PUT',
            url: `/home/EditMember?index=${index}&name=${newName}`,
            success: function (data) {
                $('.name').get(index).replaceWith(newName);
            },
            error: function (data) {
                alert(`failed to edit ${index}`);
            },
        });

    })
});