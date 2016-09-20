var api = 'http://' + location.host + '/api/',
    dietId = '',
    userId = '',
    progress = {
        init: function () {
            dietId = $('#dietId').val();
            userId = $('#userId').val();
        },
        addDietProgress: function (e, fId, dId, intake, uId) {
            var target = $($(e.target).parents('.parent')[0]),
                dietProgress = target.data('dietprogress');
            if (!target.hasClass('done') && dietProgress == '') {
                $.ajax({
                    type: 'POST',
                    url: api + 'DietProgressApi/AddProgress',
                    data: JSON.stringify({ foodId: fId, dietId: dId, intake: intake, userId: uId }),
                    contentType: "application/json",
                    success: function (data) {
                        target.data('dietprogress', data.Id);
                    }
                })

                target.addClass('done');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: api + 'DietProgressApi/RemoveProgress?id=' + dietProgress.trim()
                });
                target.data('dietprogress', '');
                target.removeClass('done');
            }
        },
        mouseOver: function (th, t) {
            if ($(th).length > 0 && $(t).length > 0) {
                $(th).hide();
                $(t).show();
            }
        }
    };