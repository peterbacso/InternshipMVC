// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?memberName=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`<li class="member">
		            <span class="name">${newcomerName}</span><span class="delete fa fa-remove"></span><i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
		        </li>`);

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

    // Bind event to dynamically created element: https://makitweb.com/attach-event-to-dynamically-created-elements-with-jquery
    $("#list").on("click", ".delete", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index(targetMemberTag.parent());
        $.ajax({
            url: `/Home/RemoveMember/${index}`,
            type: 'DELETE',
            success: function () {
                targetMemberTag.remove();
            },
            error: function () {
                alert(`Failed to delete member with index=${index}`);
            }
        })
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        var name = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");
        console.log(`/Home/EditMember?index=${index}&name=${name}`);
        $.ajax({
            url: `/Home/EditMember?index=${index}&name=${name}`,
            type: 'PUT',
            success: function () {
                location.reload();
            },
            error: function () {
                alert(`Failed to replace member ${name}`);
            }
        })
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })

    function refreshWeatherForecast() {
        $.ajax({
            url: `/WeatherForecast`,
            success: function (data) {
                let tommorow = data[0];
                let tommorowDate = formatDate(tommorow.date);

                $('#date').text(tommorowDate);
                $('#temperature').text(tommorow.temperatureC, 'C');
                $('#summary').text(tommorow.summary);
            },
            error: function (data) {
                alert(`Failed to load date`);
            },
        });
    }

    refreshWeatherForecast();

    setInterval(refreshWeatherForecast, 5000);
   
    function formatDate(jsonDate) {

        function join(t, a, s) {
            function format(m) {
                let f = new Intl.DateTimeFormat('en', m);
                return f.format(t);
            }
            return a.map(format).join(s);
        }

        let date = new Date(jsonDate);
        let a = [{ day: 'numeric' }, { month: 'short' }, { year: 'numeric' }];
        let s = join(date, a, '-');
        return s;
    }
});