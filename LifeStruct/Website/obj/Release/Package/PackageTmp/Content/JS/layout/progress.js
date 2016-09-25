var api = 'http://' + location.host + '/api/',
    dietId = '',
    userId = '',
    progress = {
        init: function () {
            dietId = $('#dietId').val();
            userId = $('#userId').val();
            var total = $('#total').val();
            $('.total').append(total)
        },
        addDietProgress: function (e, fId, dId, intake, uId, meal) {
            var target = $($(e.target).parents('.parent')[0]),
                dietProgress = target.data('dietprogress');
            if (!target.hasClass('done') && dietProgress == '') {
                $.ajax({
                    type: 'POST',
                    url: api + 'DietProgressApi/AddProgress',
                    data: JSON.stringify({ foodId: fId, dietId: dId, intake: intake, userId: uId, meal: meal }),
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
        addFitnessProgress: function (e, eId, fId, loss, uId, eIndex) {
            var target = $(e.target);
            if (!target.hasClass('done') && target.data('fitnessprogress') == '') {
                $.ajax({
                    type: 'POST',
                    url: api + 'FitnessApi/SetProgress',
                    data: JSON.stringify({ exerciseId: eId, fitnessId: fId, loss: loss, userId: uId, exerciseIndex: eIndex }),
                    contentType: "application/json",
                    success: function (data) {
                        target.data('fitnessprogress', data.Id)
                    }
                });
                target.addClass('done');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: api + 'FitnessApi/RemoveProgress?id=' + target.data('fitnessprogress').trim()
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