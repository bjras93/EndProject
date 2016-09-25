var api = 'http://' + location.host + '/api/';
var update = {
    set: function (uId, type, target) {
        $.ajax({
            method: 'GET',
            url: api + 'UpdateApi/Set' + type + '?id=' + uId,
            success: function (data) {
                if(data == "set")
                {
                    $('body').removeClass('stop-scroll');
                    $('.mood-overlay').hide();
                }
            }
        });
        $('.goal').children('.tile').each(function () {
            $(this).removeClass('selected');
        });
        $(target).addClass('selected')
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
        $.ajax({
            method: 'GET',
            url: api + 'UpdateApi/GetMood?uId=' + uId,
            success: function (data) {
                if(data == 'notset')
                {
                    $('body').addClass('stop-scroll');
                    $('.mood-overlay').show();
                }
            }
        })
    }
};
