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
                        <span class="name">${data}</span><span class="fa fa-pencil"></span><i class="delete fa fa-remove"></i>
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
        var parent = $(this).parent('li');
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
});