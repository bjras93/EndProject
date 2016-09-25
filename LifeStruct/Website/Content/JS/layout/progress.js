var api = 'http://' + location.host + '/api/',
    dietId = '',
    userId = '',
    gain = 0,
    loss = 0,
    progress = {
        init: function () {
            dietId = $('#dietId').val();
            userId = $('#userId').val();
            $.ajax({
                type: 'GET',
                url: api + 'DietProgressApi/GetProgress',
                success: function (data) {
                    console.log(data)
                    gain = 0;
                    for (var i = 0; i < data.DietProgress.length; i++) {
                        gain += parseFloat(data.DietProgress[i].CalorieIntake);
                    }
                    for (var i = 0; i < data.FitnessProgress.length; i++) {
                        loss += parseFloat(data.FitnessProgress[i].Loss)
                    }
                    $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                    $('#loss').text('Calorie loss: ' + parseFloat(loss).toFixed(0))
                }
            })
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
                        gain += parseFloat(data.CalorieIntake);
                        $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                        target.data('dietprogress', data.Id);

                        setTimeout(function () {
                            $('.intakeHover').trigger('mouseover')
                        }, 200)
                        setTimeout(function () {

                            $('.intakeHover').trigger('mouseleave')
                        }, 1500)
                    }
                })

                target.addClass('done');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: api + 'DietProgressApi/RemoveProgress?id=' + dietProgress.trim(),
                    success: function (data) {
                        console.log(data)
                        gain = gain - parseFloat(data);
                        $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                        setTimeout(function () {
                            $('.intakeHover').trigger('mouseover')
                        }, 200)
                        setTimeout(function () {

                            $('.intakeHover').trigger('mouseleave')
                        }, 1500)
                    }
                });

                target.data('dietprogress', '');
                target.removeClass('done');
            }
        },
        addFitnessProgress: function (e, eId, fId, loss, uId, eIndex) {
            var target = $(e.target);
            console.log(target.data('fitnessprogress'))
            if (!target.hasClass('done') && target.data('fitnessprogress') == '') {
                $.ajax({
                    type: 'POST',
                    url: api + 'FitnessApi/SetProgress',
                    data: JSON.stringify({ exerciseId: eId, fitnessId: fId, loss: parseFloat(loss).toFixed(0), userId: uId, exerciseIndex: eIndex }),
                    contentType: "application/json",
                    success: function (data) {
                        target.data('fitnessprogress', data.Id)
                        loss += parseFloat(data.Loss);
                        $('#loss').text('Calorie loss: ' + parseFloat(loss).toFixed(0))
                    }
                });
                target.addClass('done');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: api + 'FitnessApi/RemoveProgress?id=' + target.data('fitnessprogress').trim(),
                    success: function (data) {
                        loss = loss - parseFloat(data);
                        $('#loss').text('Calorie loss: ' + parseFloat(data).toFixed(0))
                    }
                });
                target.data('fitnessprogress', '');
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