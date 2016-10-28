var api = 'http://' + location.host + '/api/',
    dietId = '',
    userId = '',
    gain = 0,
    fLoss = 0,
    total = 0,
    progress = {
        init: function () {
            dietId = $('#dietId').val();
            userId = $('#userId').val();
            $.ajax({
                type: 'GET',
                url: api + 'DietProgressApi/GetProgress',
                success: function (data) {
                    if (data.DietProgress != null) {
                        for (var i = 0; i < data.DietProgress.length; i++) {
                            gain += parseFloat(data.DietProgress[i].CalorieIntake);
                        }
                    } else {
                        gain = 0;
                    }

                    if (data.FitnessProgress != null) {
                        for (var i = 0; i < data.FitnessProgress.length; i++) {
                            fLoss += parseFloat(data.FitnessProgress[i].Loss);
                        }
                    } else {
                        fLoss = 0;
                    }
                    $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                    $('#loss').text('Calorie loss: ' + parseFloat(fLoss).toFixed(0));
                    total = parseFloat(data.Bmr).toFixed(0) - gain;
                    $('#total').text('Suggested intake: ' + total);
                }
            });
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
                        var fix = data.CalorieIntake.replace(',', '.');
                        gain += +parseFloat(fix);
                        $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                        total = total - parseFloat(data.CalorieIntake);
                        $('#total').text('Suggested intake: ' + parseFloat(total).toFixed(0));

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
                        gain = gain - +parseFloat(data);
                        $('#gain').text('Calorie intake: ' + parseFloat(gain).toFixed(0));
                        total += +parseFloat(data);
                        $('#total').text('Suggested intake: ' + parseFloat(total).toFixed(0));
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
            if (target.parents('.home-exercise').length > 0)
            {
                target = $(target.parents('.home-exercise')[0]);
            }

            if (!target.hasClass('done') && target.data('fitnessprogress') == '') {
                $.ajax({
                    type: 'POST',
                    url: api + 'FitnessApi/SetProgress',
                    data: JSON.stringify({ exerciseId: eId, fitnessId: fId, loss: parseFloat(loss).toFixed(0), userId: uId, exerciseIndex: eIndex }),
                    contentType: "application/json",
                    success: function (data) {
                        target.data('fitnessprogress', data.Id)
                        fLoss += +parseFloat(data.Loss);
                        total = total + +parseFloat(data.Loss);
                        $('#total').text('Suggested intake: ' + parseFloat(total).toFixed(0));
                        $('#loss').text('Calorie loss: ' + parseFloat(fLoss).toFixed(0))
                        setTimeout(function () {
                            $('.intakeHover').trigger('mouseover')
                        }, 200)
                        setTimeout(function () {

                            $('.intakeHover').trigger('mouseleave')
                        }, 1500)
                    }
                });
                target.addClass('done');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: api + 'FitnessApi/RemoveProgress?id=' + target.data('fitnessprogress'),
                    success: function (data) {
                        fLoss = fLoss - parseFloat(data);
                        total = total - parseFloat(data);
                        $('#total').text('Suggested intake: ' + parseFloat(total).toFixed(0));
                        $('#loss').text('Calorie loss: ' + parseFloat(fLoss).toFixed(0))
                        setTimeout(function () {
                            $('.intakeHover').trigger('mouseover')
                        }, 200)
                        setTimeout(function () {

                            $('.intakeHover').trigger('mouseleave')
                        }, 1500)
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
