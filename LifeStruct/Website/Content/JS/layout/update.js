var api = 'http://' + location.host + '/api/';
var update = {
    set: function (uId, type, target) {
        $('.goal').children('.tile').each(function () {
            $(this).removeClass('selected');
        });
        $(target).addClass('selected')
        $.ajax({
            method: 'GET',
            url: api + 'UpdateApi/Set' + type + '?id=' + uId
        });
    },
    get: function (uId) {
        $.ajax({
            method: 'GET',
            url: api + 'UpdateApi/GetGoal?uId=' + uId,
            success: function (data) {
                $('#goal' + data.Goal).addClass('selected');
            },
            error: function () {
                setTimeout(function () {
                    $('.goal').toggle("slide", { direction: "right" }, 1500);
                }, 2000)
            }
        });
    }
};
