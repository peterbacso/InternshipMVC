// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            method: "POST",
            url: `/api/Internship/`,
            contentType: 'application/json',
            data: JSON.stringify({ "name": `${newcomerName}` }),
            success: function (data) {
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

     $("#list").on("click", ".delete", function () {
        var $li = $(this).closest('li');
         var id = $li.attr('member-id');
         console.log(id);

        $.ajax({
            method: "DELETE",
            url: `/api/Internship/${id}`,
            success: function (data) {
                $li.remove();
            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var serverIndex = targetMemberTag.attr('member-id');
        var clientIndex = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("member-id", serverIndex);
        $('#editClassmate').attr("memberIndex", clientIndex);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        console.log('submit changes to server');

        var newName = $('#classmateName').val();
        var id = $('#editClassmate').attr("member-id");
        var index = $('#editClassmate').attr("memberIndex");

        $.ajax({
            type: "PUT",
            url: `/api/Internship/${id}`,
            contentType: 'application/json',
            data: JSON.stringify({ "id":`${id}`, "name": `${newName}` }),
            success: function (response) {
                $('.name').eq(index).replaceWith(newName);
            },
            error: function (data) {
                alert(`Failed to update`);
            }
        });
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